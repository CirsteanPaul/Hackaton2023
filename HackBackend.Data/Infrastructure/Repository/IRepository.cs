using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace HackBackend.Data.Infrastructure.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        bool Any(Expression<Func<TEntity, bool>> where);
        int Count();
        int Count(Expression<Func<TEntity, bool>> where);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAll(int startRowIndex, int maximumRows, string sortColumn, SortDirection sortDirection);
        TEntity Find(int id);
        TEntity Find(string id);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, int rowCount);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, int startRowIndex, int maximumRows, string sortColumn, SortDirection sortDirection);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
