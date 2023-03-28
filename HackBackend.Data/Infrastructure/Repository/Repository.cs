using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using HackBackend.Data.Extensions;

namespace HackBackend.Data.Infrastructure.Repository
{
    public enum SortDirection
    {
        Ascending,
        Descending
    }

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            Context = context;
        }

        public bool Any(Expression<Func<TEntity, bool>> where)
        {
            return Context.Set<TEntity>().Where(where).Any();
        }

        public int Count()
        {
            return Context.Set<TEntity>().Count();
        }

        public int Count(Expression<Func<TEntity, bool>> where)
        {
            return Context.Set<TEntity>().Where(where).Count();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        public IEnumerable<TEntity> GetAll(int startRowIndex, int maximumRows, string sortColumn, SortDirection sortDirection)
        {
            return Context.Set<TEntity>().OrderBy(sortColumn, sortDirection).Skip(startRowIndex).Take(maximumRows).ToList();
        }

        public TEntity Find(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public TEntity Find(string id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate).ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, int rowCount)
        {
            return Context.Set<TEntity>().Where(predicate).Take(rowCount).ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, int startRowIndex, int maximumRows, string sortColumn, SortDirection sortDirection)
        {
            return Context.Set<TEntity>().Where(predicate).OrderBy(sortColumn, sortDirection).Skip(startRowIndex).Take(maximumRows);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().SingleOrDefault(predicate);
        }

        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        public void Update(TEntity entity)
        {
            Context.Set<TEntity>().Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }
    }
}
