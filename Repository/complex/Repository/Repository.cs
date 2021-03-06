﻿//using Conplex_IRepository = EntityFrameWork.Server.Repository.complex.IRepository.IRepository<class>;

using EntityFrameWork.Server.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace EntityFrameWork.Server.Repository.complex.Repository
{
    /// <summary>
    /// 暂时找不到很好的解决方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T>: IRepository.IRepository<T> where T : class, new()
    {
        private readonly AppDbContext _context;
        public Repository(AppDbContext myContext)
        {
            _context = myContext;
        }

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        /// <summary>
        /// 新增实体列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        public void AddRange(IEnumerable<T> entity) 
        {
            _context.Set<T>().AddRange(entity);
        }

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        public void Modify(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// 修改实体列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="where">条件</param>
        /// <param name="entity"></param>
        public void ModifyRange(Expression<Func<T, bool>> where, Expression<Func<T, T>> entity)
        {
            var entity_list = _context.Set<T>().Where(where);
            foreach (var item in entity_list)
            {
                _context.Entry(item).State = EntityState.Modified;
            }
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        /// <summary>
        ///  Any 查找数据判断数据是否存在
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public bool Any(Expression<Func<T, bool>> where)
        {
            return _context.Set<T>().Any(where);
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        /// <summary>
        /// 根据条件查询实体列表
        /// </summary>
        /// <param name="where">lamda表达式</param>
        /// <returns></returns>
        public IQueryable<T> GetList (Expression<Func<T, bool>> where)
        {
            return _context.Set<T>().Where(where);
        }

        /// <summary>
        /// 加载自己定义排序分页实体列表
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="orderby">排序</param>
        /// <param name="asc">asc/desc</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">分页的条数</param>      
        /// <returns></returns>
        public IQueryable<T> GetList(Expression<Func<T, bool>> where, Expression<Func<T, dynamic>> orderby, string asc, int pageIndex, int pageSize)
        {
            var entity = _context.Set<T>().Where(where);
            if (asc.ToLower().Equals("asc"))
                entity = entity.OrderBy(orderby);
            else
                entity = entity.OrderByDescending(orderby);

            return entity.Skip(pageIndex - 1).Take(pageSize);
        }

        /// <summary>
        /// 根据条件查询实体列表不跟踪
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public IQueryable<T> GetListAsNoTracking(Expression<Func<T, bool>> where)
        {
            return _context.Set<T>().AsNoTracking().Where(where);
        }

        /// <summary>
        /// 获取最大的一条数据
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public T MaxSort(Expression<Func<T, dynamic>> order)
        {
            return _context.Set<T>().OrderByDescending(order).FirstOrDefault();
        }

        /// <summary>
        /// 求和
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <param name="sum"></param>
        /// <returns></returns>
        public T GetSum(Expression<Func<T, bool>> where, Expression<Func<T, dynamic>> sum)
        {
            throw new NotImplementedException();//_context.Set<T>().Where(where).Sum()
        }

        /// <summary>
        /// 获取最大
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        public T GetMax(Expression<Func<T, dynamic>> max)
        {
            //throw new NotImplementedException();
            return _context.Set<T>().Max(max);
        }

        /// <summary>
        /// 使用sql脚本查询实体列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public IQueryable<T> GetListlBySql(string sql, params object[] param)
        {
            //_context.Database.SqlQuery(sql, param);
            throw new NotImplementedException();
        }

        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="sql"></param>
        public void ExecFromSql(string sql)
        {
            _context.Database.ExecuteSqlCommand(sql);
        }

        /// <summary>
        /// 主从同步更换数据库连接
        /// </summary>
        public void SqlMasterSlaveConn()
        {
            //_context.Database.Connection.ConnectionString = string.Empty;
            throw new NotImplementedException();
        }
    }
}
