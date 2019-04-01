using EntityFrameWork.Server.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameWork.Server.UnitOfWork.Simple.IRepsitory
{
    /// <summary>
    /// 仓储基类中接口
    /// </summary>
    public interface IRepository
    {

        //DbFunctions.AsNonUnicode  生成sql后没有N

        #region 新增

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void Add<T>(T entity);

        /// <summary>
        /// 异步新增实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task AddAsync<T>(T entity);

        /// <summary>
        /// 新增实体列表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void AddRange<T>(IEnumerable<T> entity);

        /// <summary>
        /// 异步新增实体列表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task AddRangeAsync<T>(IEnumerable<T> entity);

        #endregion

        #region 删除

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        int Delete<T>(Expression<Func<T, bool>> where);

        /// <summary>
        /// 异步删除实体
        /// </summary>
        /// <param name="where">实体</param>
        /// <returns></returns>
        Task<int> DeleteAsync<T>(Expression<Func<T, bool>> where);

        #endregion

        #region 修改

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void Modify<T>(T entity);

        int ModifyRange<T>(Expression<Func<T, bool>> where, Expression<Func<T, T>> entity);

        Task<int> ModifyRangeAsync<T>(Expression<Func<T, bool>> where, Expression<Func<T, T>> entity);

        #endregion

        #region 查询

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        List<T> GetAll<T>();

        /// <summary>
        /// 异步查询所有数据
        /// </summary>
        /// <returns></returns>
        Task<List<T>> GetAllAsync<T>();

        /// <summary>
        /// 根据条件查询实体列表
        /// </summary>
        /// <param name="where">lamda表达式</param>
        /// <returns></returns>
        IEnumerable<T> GetList<T>(Expression<Func<T, bool>> where);

        /// <summary>
        /// 根据条件查询实体列表不跟踪
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        List<T> GetListAsNoTracking<T>(Expression<Func<T, bool>> where);

        /// <summary>
        /// 异步根据条件查询实体列表不跟踪
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<List<T>> GetListAsNoTrackingAsync<T>(Expression<Func<T, bool>> where);

        /// <summary>
        /// 根据加载实体
        /// </summary>
        /// <param name="where">lamda表达式</param>
        /// <returns></returns>
        T WhereLoadEntity<T>(Expression<Func<T, bool>> where);

        /// <summary>
        /// 根据条件异步加载实体
        /// </summary>
        /// <param name="where">lamda表达式</param>
        /// <returns></returns>
        Task<T> WhereLoadEntityAsync<T>(Expression<Func<T, bool>> where);

        /// <summary>
        /// 根据条件查询实体不跟踪
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        T WhereLoadEntityAsNoTracking<T>(Expression<Func<T, bool>> where);

        /// <summary>
        /// 异步根据条件异步查询实体不跟踪
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<T> WhereLoadEntityAsNoTrackingAsync<T>(Expression<Func<T, bool>> where);
        /// <summary>
        /// 获取最大的一条数据
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        T LoadEntityMaxSort<T>(Expression<Func<T, int>> order);
        /// <summary>
        /// 异步获取最大的一条数据
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        Task<T> LoadEntityMaxSortAsync<T>(Expression<Func<T, int>> order);

        /// <summary>
        ///  Any 查找数据判断数据是否存在
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        bool AnyEntity<T>(Expression<Func<T, bool>> where);

        /// <summary>
        ///  异步Any 查找数据判断数据是否存在
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<bool> AnyEntityAsync<T>(Expression<Func<T, bool>> where);

        /// <summary>
        /// 加载自己定义排序分页实体列表
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="orderby">排序</param>
        /// <param name="asc">asc/desc</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">分页的条数</param>      
        /// <returns></returns>
        IEnumerable<T> LoadEntityEnumerable<T>(Expression<Func<T, bool>> where, Expression<Func<T, string>> orderby, string asc, int pageIndex, int pageSize);
        IEnumerable<T> LoadEntityEnumerable<T>(Expression<Func<T, bool>> where, Expression<Func<T, int?>> orderby, string asc, int pageIndex, int pageSize);
        IEnumerable<T> LoadEntityEnumerable<T>(Expression<Func<T, bool>> where, Expression<Func<T, DateTime?>> orderby, string asc, int pageIndex, int pageSize);
        IEnumerable<T> LoadEntityEnumerable<T>(Expression<Func<T, bool>> where, Expression<Func<T, decimal?>> orderby, string asc, int pageIndex, int pageSize);
        IEnumerable<T> LoadEntityEnumerable<T>(Expression<Func<T, bool>> where, Expression<Func<T, bool?>> orderby, string asc, int pageIndex, int pageSize);


        /// <summary>
        /// 加载自己定义排序分页实体列表
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="orderby">排序</param>
        /// <param name="asc">asc/desc</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">分页的条数</param>      
        /// <returns></returns>
        Task<List<T>> LoadEntityListAsync<T>(Expression<Func<T, bool>> where, Expression<Func<T, string>> orderby, string asc, int pageIndex, int pageSize);
        Task<List<T>> LoadEntityListAsync<T>(Expression<Func<T, bool>> where, Expression<Func<T, int?>> orderby, string asc, int pageIndex, int pageSize);
        Task<List<T>> LoadEntityListAsync<T>(Expression<Func<T, bool>> where, Expression<Func<T, DateTime?>> orderby, string asc, int pageIndex, int pageSize);
        Task<List<T>> LoadEntityListAsync<T>(Expression<Func<T, bool>> where, Expression<Func<T, decimal?>> orderby, string asc, int pageIndex, int pageSize);


        #region 求平均，求总计
        int? GetSum<T>(Expression<Func<T, bool>> where, Expression<Func<T, int?>> sum);
        double? GetSum<T>(Expression<Func<T, bool>> where, Expression<Func<T, double?>> sum);
        float? GetSum<T>(Expression<Func<T, bool>> where, Expression<Func<T, float?>> sum);
        decimal? GetSum<T>(Expression<Func<T, bool>> where, Expression<Func<T, decimal?>> sum);
        double? GetAvg<T>(Expression<Func<T, bool>> where, Expression<Func<T, int?>> avg);
        double? GetAvg<T>(Expression<Func<T, bool>> where, Expression<Func<T, double?>> avg);
        float? GetAvg<T>(Expression<Func<T, bool>> where, Expression<Func<T, float?>> avg);
        decimal? GetAvg<T>(Expression<Func<T, bool>> where, Expression<Func<T, decimal?>> avg);

        #endregion

        #region 查最大
        /// <summary>
        /// 获取最大
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        int? GetMax<T>(Expression<Func<T, int?>> max);
        /// <summary>
        /// 获取最大
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        double? GetMax<T>(Expression<Func<T, double?>> max);
        /// <summary>
        /// 获取最大
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        decimal? GetMax<T>(Expression<Func<T, decimal?>> max);
        /// <summary>
        /// 获取最大
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        DateTime? GetMax<T>(Expression<Func<T, DateTime?>> max);

        #endregion
        /// <summary>
        /// 查询实体数量
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        int GetEntitiesCount<T>(Expression<Func<T, bool>> where);
        /// <summary>
        /// 异步查询实体数量
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<int> GetEntitiesCountAsync<T>(Expression<Func<T, bool>> where);
        /// <summary>
        /// 使用sql脚本查询实体列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        List<T> GetModeListlBySql<T>(string sql);

        /// <summary>
        /// 使用sql脚本异步查询实体列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        Task<List<T>> GetModeListlBySqlAsync<T>(string sql);

        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="sql"></param>
        void ExecFromSql<T>(string sql);

        #endregion
        /// <summary>
        /// 主从同步更换数据库连接
        /// </summary>
        void SqlMasterSlaveConn();
    }
}
