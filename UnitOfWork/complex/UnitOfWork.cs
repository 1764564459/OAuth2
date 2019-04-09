using EntityFrameWork.Server.Entity;
using System;
using System.Threading.Tasks;
//using AppRepository = EntityFrameWork.Server.Repository.complex.Repository.Repository<dynamic>;

namespace EntityFrameWork.Server.UnitOfWork.complex
{
    /// <summary>
    /// 暂时找不到很好的解决方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UnitOfWork:IUnitOfWork
    {
        public Repository.complex.Repository.Repository<UnitOfWork> _Repository;//操作上下文储存仓库
        private AppDbContext _context { get; set; }//数据库上下文

        /// <summary>
        /// 初始化上下文、储存仓库
        /// </summary>
        /// <param name="context"></param>
        public UnitOfWork()
        {
            _context = new AppDbContext();
            _Repository = new Repository.complex.Repository.Repository<UnitOfWork>(_context);
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
