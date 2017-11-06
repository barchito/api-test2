using Core2APIExercise.Common.Interfaces;
using Core2APIExercise.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Core2APIExercise.Common.Repositories
{
    public class Repository<T, TContext> : IRepository<T> where T : BaseEntity where TContext : DbContext
    {
        private readonly TContext context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;

        public Repository()
        {
        }
        public Repository(TContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }
        public T Add(T entity)
        {
            context.Add<T>(entity);
            return entity;
        }

        public async Task<T> AddAsync(T entity)
        {
          var a= await entities.AddAsync(entity);
            return a.Entity;
        }

        public bool Delete(T entity)
        {
            entities.Remove(entity);
            return true;
        }

        public async Task<int> ExecuteSqlCommandAsync(string queryString)
        {
            return await context.Database.ExecuteSqlCommandAsync(queryString);
        }

        public IQueryable<T> Get(Guid id)
        {
            return entities.Where(s => s.ID == id).AsQueryable<T>();
        }

        public IQueryable<T> GetAll(Func<IQueryable<T>, IQueryable<T>> func)
        {
            DbSet<T> result = context.Set<T>();
            IQueryable<T> resultWithEagerLoading = func(result);
            return entities.AsQueryable<T>();
        }
        public IQueryable<T> GetAll()
        {
            return entities.AsQueryable<T>();
        }
        public DbConnection GetDbConnection()
        {
            return context.Database.GetDbConnection();
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public bool Update(T entity)
        {
            //context.Entry(entity).Property("RowVersion").OriginalValue = rowVersion;
            context.Update(entity);
            return true;
        }
        // Test Scenario
        public bool Update(T entity,byte[] rowVersion)
        {
            context.Entry(entity).Property("RowVersion").OriginalValue = rowVersion;
            context.Update(entity);
            return true;
        }
    }
}
