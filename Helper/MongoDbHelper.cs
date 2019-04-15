using EntityFrameWork.Server.Entity.Auth;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameWork.Server.Helper
{
    public class MongoDbHelper//<T> where T : class
    {
        private IMongoDatabase db = null;
        

        private string data_base = "MongoApp";
        private string data_collect = "AppUser";
        private string data_address = "mongodb://localhost:27017";
        private static readonly object lockHelper = new object();
        MongoClient client;
        public MongoDbHelper()
        {
            if (db == null)
            {
                lock (lockHelper)
                {
                    if (db == null)
                    {
                        client = new MongoClient(data_address);
                        db = client.GetDatabase(data_base);
                    }
                }
            }
            //collection = db.GetCollection<T>(data_collect);//(typeof(T).Name);
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> Insert<T>(T entity)
        {
            //var flag = ObjectId.GenerateNewId();
            //entity.GetType().GetProperty("Name").GetValue(entity);//获取Name值
            await db.GetCollection<T>(typeof(T).Name).InsertOneAsync(entity);
            //await collection.InsertOneAsync(entity);
            return entity;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        public async Task Modify<T>(string id, string field, string value)
        {
            var filter = Builders<T>.Filter.Eq("Id", ObjectId.Parse(id));
            var updated = Builders<T>.Update.Set(field, value);
            //UpdateResult result = collection.UpdateOneAsync(filter, updated).Result;
            UpdateResult result = await db.GetCollection<T>(typeof(T).Name).UpdateOneAsync(filter, updated);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public async Task Update<T>(T entity)
        {
            try
            {
                var p = entity.GetType().GetProperties().Where(o => o.Name.ToLower().Equals("id")).FirstOrDefault();

                var id = entity.GetType().GetProperty(p.Name).GetValue(entity).ToString();
                var filter = Builders<T>.Filter.Eq("Id", ObjectId.Parse(id));
                var old = db.GetCollection<T>(typeof(T).Name).Find(filter).ToList().FirstOrDefault();

                foreach (var prop in entity.GetType().GetProperties())
                {
                    var newValue = prop.GetValue(entity);
                    var oldValue = old.GetType().GetProperty(prop.Name).GetValue(old);
                    if (newValue != null)
                    {
                        if (oldValue == null)
                            oldValue = "";
                        if (!newValue.ToString().Equals(oldValue.ToString()))
                        {
                            old.GetType().GetProperty(prop.Name).SetValue(old, newValue.ToString());
                        }
                    }
                }
                //old.State = "n";
                //old.UpdateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                filter = Builders<T>.Filter.Eq("Id", id);//entity.Id);
                ReplaceOneResult result = await db.GetCollection<T>(typeof(T).Name).ReplaceOneAsync(filter, old); //collection.ReplaceOneAsync(filter, old).Result;
            }
            catch (Exception ex)
            {
                //var aaa = ex.Message + ex.StackTrace;
                throw ex;
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        public void Delete<T>(T entity)
        {
            var _Id = entity.GetType().GetProperties().Where(p => p.Name.ToLower().Equals("_id")).FirstOrDefault().GetValue(entity);
            var filter = Builders<T>.Filter.Eq("Id", _Id);
            db.GetCollection<T>(typeof(T).Name).DeleteOneAsync(filter);
        }
        /// <summary>
        /// 根据id查询一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T QueryOne<T>(string id)
        {
            var filter = Builders<T>.Filter.Eq("Id",ObjectId.Parse(id));
            return db.GetCollection<T>(typeof(T).Name).Find(filter).FirstOrDefault();
            //return collection.Find(a => a.Id == ObjectId.Parse(id)).ToList().FirstOrDefault();
        }
        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> QueryAll<T>()
        {
            var filter = Builders<T>.Filter.Empty;//.Ne("Name", "");
            return db.GetCollection<T>(typeof(T).Name).Find(filter).ToEnumerable();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="page_index">页码</param>
        /// <param name="page_size">每页数量</param>
        /// <param name="express">条件</param>
        /// <param name="sort_field">排序字段</param>
        /// <param name="asc_desc">升序降序</param>
        /// <returns></returns>
        public IEnumerable<T> GetList<T>(int page_index,int page_size, Expression<Func<T, bool>> express, Expression<Func<T, dynamic>> sort_field,string asc_desc)
        {
            var filter = Builders<T>.Filter.And(express);
            //条件查询所有
            var entity = db.GetCollection<T>(typeof(T).Name).Find(filter).Skip(page_index-1).Limit(page_size);
            if (asc_desc.ToLower().Contains("asc"))
                entity = entity.SortBy(sort_field);
            else
                entity = entity.SortByDescending(sort_field);
            return entity.ToEnumerable();
        }
        /// <summary>
        /// 根据条件查询一条数据
        /// </summary>
        /// <param name="express"></param>
        /// <returns></returns>
        public T QueryByFirst<T>(Expression<Func<T, bool>> express)
        {
            return db.GetCollection<T>(typeof(T).Name).Find(express).FirstOrDefault();
        }
        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="list"></param>
        public async Task InsertBatch<T>(IEnumerable<T> list)
        {
           await db.GetCollection<T>(typeof(T).Name).InsertManyAsync(list);
        }
        /// <summary>
        /// 根据Id批量删除
        /// </summary>
        public async Task DeleteBatch<T>(IEnumerable<ObjectId> list)
        {
            var filter = Builders<T>.Filter.In("Id", list);
            await db.GetCollection<T>(typeof(T).Name).DeleteManyAsync(filter);
        }

        /// <summary>
        /// 未添加到索引的数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> QueryToLucene<T>()
        {
            //return new List<T>();
            return db.GetCollection<T>(typeof(T).Name).Find(null).ToEnumerable();
        }
    }
    [Serializable]
    public class MyApp
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime? Time { get; set; }
        [JsonIgnore]
        public IEnumerable<AppUser> users { get; set; }
    }
}
