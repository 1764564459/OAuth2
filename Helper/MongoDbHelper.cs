using MongoDB.Bson;
using MongoDB.Driver;
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


        private string data_base = "Vehicle";
        private string data_collect = "Real";
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
            var flag = ObjectId.GenerateNewId();
            entity.GetType().GetProperty("Name").GetValue(entity);//获取Name值
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
        public void Modify<T>(string id, string field, string value)
        {
            var filter = Builders<T>.Filter.Eq("Id", ObjectId.Parse(id));
            var updated = Builders<T>.Update.Set(field, value);
            //UpdateResult result = collection.UpdateOneAsync(filter, updated).Result;
            UpdateResult result = db.GetCollection<T>(typeof(T).Name).UpdateOneAsync(filter, updated).Result;
            //result.
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update<T>(T entity)
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
                ReplaceOneResult result = db.GetCollection<T>(typeof(T).Name).ReplaceOneAsync(filter, old).Result; //collection.ReplaceOneAsync(filter, old).Result;
            }
            catch (Exception ex)
            {
                var aaa = ex.Message + ex.StackTrace;
                throw;
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        public void Delete<T>(T entity)
        {
            var id_val = entity.GetType().GetProperties().Where(p => p.Name.ToLower().Equals("_id")).FirstOrDefault().GetValue(entity);
            var filter = Builders<T>.Filter.Eq("Id", id_val);
            db.GetCollection<T>(typeof(T).Name).DeleteOneAsync(filter);
        }
        /// <summary>
        /// 根据id查询一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T QueryOne<T>(string id)
        {
            var filter = Builders<T>.Filter.Eq("", "");
            return db.GetCollection<T>(typeof(T).Name).Find(filter).FirstOrDefault();
            //return collection.Find(a => a.Id == ObjectId.Parse(id)).ToList().FirstOrDefault();
        }
        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public List<T> QueryAll<T>()
        {
            var filter = Builders<T>.Filter.Ne("Name", "");
            return db.GetCollection<T>(typeof(T).Name).Find(filter).ToList();
        }
        /// <summary>
        /// 根据条件查询一条数据
        /// </summary>
        /// <param name="express"></param>
        /// <returns></returns>
        public T QueryByFirst<T>(Expression<Func<T, bool>> express)
        {
            return db.GetCollection<T>(typeof(T).Name).Find(express).ToList().FirstOrDefault();
        }
        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="list"></param>
        public void InsertBatch<T>(List<T> list)
        {
            db.GetCollection<T>(typeof(T).Name).InsertManyAsync(list);
        }
        /// <summary>
        /// 根据Id批量删除
        /// </summary>
        public void DeleteBatch<T>(List<ObjectId> list)
        {
            var filter = Builders<T>.Filter.In("Id", list);
            db.GetCollection<T>(typeof(T).Name).DeleteManyAsync(filter);
        }

        /// <summary>
        /// 未添加到索引的数据
        /// </summary>
        /// <returns></returns>
        public List<T> QueryToLucene<T>()
        {
            //return new List<T>();
            return db.GetCollection<T>(typeof(T).Name).Find(null).ToList();
        }
    }
}
