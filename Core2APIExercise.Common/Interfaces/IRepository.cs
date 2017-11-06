using Core2APIExercise.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core2APIExercise.Common.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll(Func<IQueryable<T>, IQueryable<T>> func);
        IQueryable<T> GetAll();
        IQueryable<T> Get(Guid id);
        T Add(T entity);
        Task<T> AddAsync(T entity);

        // if required for concurrency
        bool Update(T entity, byte[] rowVersion);
        bool Update(T entity);
        bool Delete(T entity);
        Task<int> ExecuteSqlCommandAsync(string queryString);
        Task SaveChangesAsync();
        DbConnection GetDbConnection();
    }
}
