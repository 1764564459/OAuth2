using EntityFrameWork.Server.Entity;
using EntityFrameWork.Server.UnitOfWork.Simple;
using System;
using System.Threading.Tasks;
using AppRepository = EntityFrameWork.Server.Repository.Simple.Repository.Repository;

namespace EntityFrameWork.Server.UnitOfWork.Simple
{
    public class UnitOfWork : IUnitOfWork
    {
        public AppRepository _Repository { get; set; }//操作上下文储存仓库
        private AppDbContext _context { get; set; }//数据库上下文

        /// <summary>
        /// 初始化上下文、储存仓库
        /// </summary>
        /// <param name="context"></param>
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            _Repository = new AppRepository(_context);
        }

        /// <summary>
        /// 提交上下文操作
        /// </summary>
        /// <returns>提交结果</returns>
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        /// <summary>
        /// 一部提交上下文操作
        /// </summary>
        /// <returns>提交结果</returns>
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
            GC.Collect();
        }

    }
}
