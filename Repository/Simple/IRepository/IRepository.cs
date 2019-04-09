using EntityFrameWork.Server.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameWork.Server.Repository.Simple.IRepsitory
{
    /// <summary>
    /// 仓储基类中接口
    /// </summary>
    public interface IRepository
    {

        //DbFunctions.AsNonUnicode  生成sql后没有N

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        void Add<T>(T entity) where T : class;


        /// <summary>
        /// 新增实体列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        void AddRange<T>(IEnumerable<T> entity) where T : class;

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        void Modify<T>(T entity) where T : class;

        /// <summary>
        /// 修改实体列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="where">条件</param>
        /// <param name="entity"></param>
        void ModifyRange<T>(Expression<Func<T, bool>> where, Expression<Func<T, T>> entity) where T : class;

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        void Delete<T>(T entity) where T : class;

        /// <summary>
        ///  Any 查找数据判断数据是否存在
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="where">条件</param>
        /// <returns></returns>
        bool Any<T>(Expression<Func<T, bool>> where) where T : class;

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll<T>() where T : class;
        

        /// <summary>
        /// 根据条件查询实体列表
        /// </summary>
        /// <param name="where">lamda表达式</param>
        /// <returns></returns>
        IQueryable<T> GetList<T>(Expression<Func<T, bool>> where) where T : class;

        /// <summary>
        /// 加载自己定义排序分页实体列表
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="orderby">排序</param>
        /// <param name="asc">asc/desc</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">分页的条数</param>      
        /// <returns></returns>
        IQueryable<T> GetList<T>(Expression<Func<T, bool>> where, Expression<Func<T, dynamic>> orderby, string asc, int pageIndex, int pageSize) where T : class;


        /// <summary>
        /// 根据条件查询实体列表不跟踪
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        IQueryable<T> GetListAsNoTracking<T>(Expression<Func<T, bool>> where) where T : class;

        /// <summary>
        /// 获取最大的一条数据
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        T MaxSort<T>(Expression<Func<T, dynamic>> order) where T : class;

        /// <summary>
        /// 求和
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <param name="sum"></param>
        /// <returns></returns>
        T GetSum<T>(Expression<Func<T, bool>> where, Expression<Func<T, dynamic>> sum) where T : class;
        
        /// <summary>
        /// 获取最大
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        T GetMax<T>(Expression<Func<T, dynamic>> max) where T : class;

        /// <summary>
        /// 使用sql脚本查询实体列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        IQueryable<T> GetListlBySql<T>(string sql,params object[] param) where T : class;
        

        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="sql"></param>
        void ExecFromSql<T>(string sql) where T : class;
        
        /// <summary>
        /// 主从同步更换数据库连接
        /// </summary>
        void SqlMasterSlaveConn();
    }
}
