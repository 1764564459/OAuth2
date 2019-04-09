using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameWork.Server.Repository.complex.IRepository
{
    public interface IRepository<T> where T : class, new()
    {
        /// <summary>
        /// 新增实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        void Add(T entity);


        /// <summary>
        /// 新增实体列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        void AddRange(IEnumerable<T> entity);

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        void Modify(T entity);

        /// <summary>
        /// 修改实体列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="where">条件</param>
        /// <param name="entity"></param>
        void ModifyRange(Expression<Func<T, bool>> where, Expression<Func<T, T>> entity);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        void Delete(T entity);

        /// <summary>
        ///  Any 查找数据判断数据是否存在
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="where">条件</param>
        /// <returns></returns>
        bool Any(Expression<Func<T, bool>> where);

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();


        /// <summary>
        /// 根据条件查询实体列表
        /// </summary>
        /// <param name="where">lamda表达式</param>
        /// <returns></returns>
        IQueryable<T> GetList(Expression<Func<T, bool>> where);

        /// <summary>
        /// 加载自己定义排序分页实体列表
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="orderby">排序</param>
        /// <param name="asc">asc/desc</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">分页的条数</param>      
        /// <returns></returns>
        IQueryable<T> GetList(Expression<Func<T, bool>> where, Expression<Func<T, dynamic>> orderby, string asc, int pageIndex, int pageSize);


        /// <summary>
        /// 根据条件查询实体列表不跟踪
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        IQueryable<T> GetListAsNoTracking(Expression<Func<T, bool>> where);

        /// <summary>
        /// 获取最大的一条数据
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        T MaxSort(Expression<Func<T, dynamic>> order);

        /// <summary>
        /// 求和
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <param name="sum"></param>
        /// <returns></returns>
        T GetSum(Expression<Func<T, bool>> where, Expression<Func<T, dynamic>> sum);

        /// <summary>
        /// 获取最大
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        T GetMax(Expression<Func<T, dynamic>> max);

        /// <summary>
        /// 使用sql脚本查询实体列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        IQueryable<T> GetListlBySql(string sql, params object[] param);


        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="sql"></param>
        void ExecFromSql(string sql);

        /// <summary>
        /// 主从同步更换数据库连接
        /// </summary>
        void SqlMasterSlaveConn();
    }
}
