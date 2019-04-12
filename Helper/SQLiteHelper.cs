using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameWork.Server.Helper
{
    //需要NutGet System.Data.SQLite
    public class SQLiteHelper : IDisposable
    {
        
        private static SQLiteConnection _SQLiteConnect { get; set; }
        //库文件夹地址
        private static string SQLite_Path = $"{Directory.GetCurrentDirectory()}//SQLite_Data";
        //库名字
        static string SQLite_Name = $"Vehicle_History.sqlite";
        //库的准确地址
        static string SQLite_File = $"{SQLite_Path}//{SQLite_Name}";
        //连接字符串
        private static readonly string str = $"Data Source ={SQLite_File}; Version=3"; //ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
        /// <summary>
        /// 建库
        /// </summary>
        public SQLiteHelper()
        {
            _SQLiteConnect = new SQLiteConnection(str);
            //文件夹是否存在
            if (!Directory.Exists(SQLite_Path))
                Directory.CreateDirectory(SQLite_Path);
            //判断数据库是否存在
            if (!File.Exists(SQLite_File))
                SQLiteConnection.CreateFile(SQLite_File);

            createTable();
        }

        /// <summary>
        /// 打开SQLite链接
        /// </summary>
        public static void Open()
        {
            if (_SQLiteConnect.State == ConnectionState.Closed)
                _SQLiteConnect.Open();
        }

        //在指定数据库中创建一个table
        void createTable()
        {
            Open();

            //检查表是否存在
            string sql = "select name from sqlite_master where name='GPS_History'";
            DataTable data = ExecuteTable(sql);
            if (data.Rows.Count > 0)
                return;

            sql = "create table GPS_History (Id GUID, Last_Time DATETIME,Data NVARCHAR,Is_Update BOOL)";

            SQLiteCommand command = new SQLiteCommand(sql, _SQLiteConnect);

            command.ExecuteNonQuery();
        }
        /// <summary>
        /// 增删改
        /// 20180723
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">sql参数</param>
        /// <returns>受影响的行数</returns>
        public int ExecuteNonQuery(string sql, params SQLiteParameter[] param)
        {
            Open();
            using (SQLiteCommand cmd = new SQLiteCommand(sql, _SQLiteConnect))
            {
                if (param != null)
                {
                    cmd.Parameters.AddRange(param);
                }

                string sql2 = cmd.CommandText;
                //con.Close();
                return cmd.ExecuteNonQuery();
            }

        }

        /// <summary>
        /// 查询
        /// 20180723
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">sql参数</param>
        /// <returns>首行首列</returns>
        public object ExecuteScalar(string sql, params SQLiteParameter[] param)
        {
            Open();
            using (SQLiteCommand cmd = new SQLiteCommand(sql, _SQLiteConnect))
            {
                if (param != null)
                {
                    cmd.Parameters.AddRange(param);
                }

                return cmd.ExecuteScalar();
            }
        }

        /// <summary>
        /// 多行查询
        /// 20180723
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">sql参数</param>
        /// <returns>SQLiteDateReader</returns>
        public SQLiteDataReader ExecuteReader(string sql, params SQLiteParameter[] param)
        {
            Open();
            using (SQLiteCommand cmd = new SQLiteCommand(sql, _SQLiteConnect))
            {
                if (param != null)
                {
                    cmd.Parameters.AddRange(param);
                }
                try
                {
                    return cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                }
                catch (Exception ex)
                {
                    _SQLiteConnect.Close();
                    _SQLiteConnect.Dispose();
                    throw ex;
                }
            }

        }

        /// <summary>
        /// 查询多行数据
        /// 20180723
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">sql参数</param>
        /// <returns>一个表</returns>
        public DataTable ExecuteTable(string sql, params SQLiteParameter[] param)
        {
            DataTable dt = new DataTable();
            using (SQLiteDataAdapter sda = new SQLiteDataAdapter(sql, str))
            {
                if (param != null)
                {
                    sda.SelectCommand.Parameters.AddRange(param);
                }
                sda.Fill(dt);
            }
            return dt;
        }

        /// <summary>
        /// 查询封装
        /// 20180725
        /// </summary>
        /// <param name="tbName">表名</param>
        /// <param name="fields">查询需要的字段名："id, name, age"</param>
        /// <param name="where">查询条件："id = 1"</param>
        /// <param name="orderBy">排序："id desc"</param>
        /// <param name="limit">分页："0,10"</param>
        /// <param name="param">sql参数</param>
        /// <returns>受影响行数</returns>
        public DataTable QueryTable(string tbName, string fields = "*", string where = "1", string orderBy = "", string limit = "", params SQLiteParameter[] param)
        {
            //排序
            if (orderBy != "")
            {
                orderBy = "ORDER BY " + orderBy;//Deom: ORDER BY id desc
            }

            //分页
            if (limit != "")
            {
                limit = "LIMIT " + limit;//Deom: LIMIT 0,10
            }

            string sql = string.Format("SELECT {0} FROM `{1}` WHERE {2} {3} {4}", fields, tbName, where, orderBy, limit);

            //return sql;
            return ExecuteTable(sql, param);

        }

        /// <summary>
        /// 数据插入
        /// 20180725
        /// </summary>
        /// <param name="tbName">表名</param>
        /// <param name="insertData">需要插入的数据字典</param>
        /// <returns>受影响行数</returns>
        public int ExecuteInsert(string tbName, Dictionary<String, String> insertData)
        {
            string point = "";//分隔符号(,)
            string keyStr = "";//字段名拼接字符串
            string valueStr = "";//值的拼接字符串

            List<SQLiteParameter> param = new List<SQLiteParameter>();
            foreach (string key in insertData.Keys)
            {
                keyStr += string.Format("{0} `{1}`", point, key);
                valueStr += string.Format("{0} @{1}", point, key);
                param.Add(new SQLiteParameter("@" + key, insertData[key]));
                point = ",";
            }
            string sql = string.Format("INSERT INTO `{0}`({1}) VALUES({2})", tbName, keyStr, valueStr);

            //return sql;
            return ExecuteNonQuery(sql, param.ToArray());

        }

        /// <summary>
        /// 执行Update语句
        /// 20180725
        /// </summary>
        /// <param name="tbName">表名</param>
        /// <param name="where">更新条件：id=1</param>
        /// <param name="insertData">需要更新的数据</param>
        /// <returns>受影响行数</returns>
        public int ExecuteUpdate(string tbName, string where, Dictionary<String, String> insertData)
        {
            string point = "";//分隔符号(,)
            string kvStr = "";//键值对拼接字符串(Id=@Id)

            List<SQLiteParameter> param = new List<SQLiteParameter>();
            foreach (string key in insertData.Keys)
            {
                kvStr += string.Format("{0} {1}=@{2}", point, key, key);
                param.Add(new SQLiteParameter("@" + key, insertData[key]));
                point = ",";
            }
            string sql = string.Format("UPDATE `{0}` SET {1} WHERE {2}", tbName, kvStr, where);

            return ExecuteNonQuery(sql, param.ToArray());

        }

        public void Dispose()
        {
            if (_SQLiteConnect != null)
                _SQLiteConnect.Dispose();
        }
    }
}
