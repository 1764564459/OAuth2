using EntityFrameWork.Server.Entity;
using EntityFrameWork.Server.UnitOfWork.Simple.IRepsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameWork.Server.UnitOfWork.Simple.Repository
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _context;
        public Repository(AppDbContext myContext)
        {
            _context = myContext;
        }

        public void Add<T>(T entity)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync<T>(T entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange<T>(IEnumerable<T> entity)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync<T>(IEnumerable<T> entity)
        {
            throw new NotImplementedException();
        }

        public bool AnyEntity<T>(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyEntityAsync<T>(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public int Delete<T>(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync<T>(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public void ExecFromSql<T>(string sql)
        {
            throw new NotImplementedException();
        }

        public List<T> GetAll<T>()
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetAllAsync<T>()
        {
            throw new NotImplementedException();
        }

        public double? GetAvg<T>(Expression<Func<T, bool>> where, Expression<Func<T, int?>> avg)
        {
            throw new NotImplementedException();
        }

        public double? GetAvg<T>(Expression<Func<T, bool>> where, Expression<Func<T, double?>> avg)
        {
            throw new NotImplementedException();
        }

        public float? GetAvg<T>(Expression<Func<T, bool>> where, Expression<Func<T, float?>> avg)
        {
            throw new NotImplementedException();
        }

        public decimal? GetAvg<T>(Expression<Func<T, bool>> where, Expression<Func<T, decimal?>> avg)
        {
            throw new NotImplementedException();
        }

        public int GetEntitiesCount<T>(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetEntitiesCountAsync<T>(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetList<T>(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public List<T> GetListAsNoTracking<T>(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetListAsNoTrackingAsync<T>(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public int? GetMax<T>(Expression<Func<T, int?>> max)
        {
            throw new NotImplementedException();
        }

        public double? GetMax<T>(Expression<Func<T, double?>> max)
        {
            throw new NotImplementedException();
        }

        public decimal? GetMax<T>(Expression<Func<T, decimal?>> max)
        {
            throw new NotImplementedException();
        }

        public DateTime? GetMax<T>(Expression<Func<T, DateTime?>> max)
        {
            throw new NotImplementedException();
        }

        public List<T> GetModeListlBySql<T>(string sql)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetModeListlBySqlAsync<T>(string sql)
        {
            throw new NotImplementedException();
        }

        public int? GetSum<T>(Expression<Func<T, bool>> where, Expression<Func<T, int?>> sum)
        {
            throw new NotImplementedException();
        }

        public double? GetSum<T>(Expression<Func<T, bool>> where, Expression<Func<T, double?>> sum)
        {
            throw new NotImplementedException();
        }

        public float? GetSum<T>(Expression<Func<T, bool>> where, Expression<Func<T, float?>> sum)
        {
            throw new NotImplementedException();
        }

        public decimal? GetSum<T>(Expression<Func<T, bool>> where, Expression<Func<T, decimal?>> sum)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> LoadEntityEnumerable<T>(Expression<Func<T, bool>> where, Expression<Func<T, string>> orderby, string asc, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> LoadEntityEnumerable<T>(Expression<Func<T, bool>> where, Expression<Func<T, int?>> orderby, string asc, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> LoadEntityEnumerable<T>(Expression<Func<T, bool>> where, Expression<Func<T, DateTime?>> orderby, string asc, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> LoadEntityEnumerable<T>(Expression<Func<T, bool>> where, Expression<Func<T, decimal?>> orderby, string asc, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> LoadEntityEnumerable<T>(Expression<Func<T, bool>> where, Expression<Func<T, bool?>> orderby, string asc, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> LoadEntityListAsync<T>(Expression<Func<T, bool>> where, Expression<Func<T, string>> orderby, string asc, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> LoadEntityListAsync<T>(Expression<Func<T, bool>> where, Expression<Func<T, int?>> orderby, string asc, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> LoadEntityListAsync<T>(Expression<Func<T, bool>> where, Expression<Func<T, DateTime?>> orderby, string asc, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> LoadEntityListAsync<T>(Expression<Func<T, bool>> where, Expression<Func<T, decimal?>> orderby, string asc, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public T LoadEntityMaxSort<T>(Expression<Func<T, int>> order)
        {
            throw new NotImplementedException();
        }

        public Task<T> LoadEntityMaxSortAsync<T>(Expression<Func<T, int>> order)
        {
            throw new NotImplementedException();
        }

        public void Modify<T>(T entity)
        {
            throw new NotImplementedException();
        }

        public int ModifyRange<T>(Expression<Func<T, bool>> where, Expression<Func<T, T>> entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> ModifyRangeAsync<T>(Expression<Func<T, bool>> where, Expression<Func<T, T>> entity)
        {
            throw new NotImplementedException();
        }

        public void SqlMasterSlaveConn()
        {
            throw new NotImplementedException();
        }

        public T WhereLoadEntity<T>(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public T WhereLoadEntityAsNoTracking<T>(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<T> WhereLoadEntityAsNoTrackingAsync<T>(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<T> WhereLoadEntityAsync<T>(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }
    }
}
