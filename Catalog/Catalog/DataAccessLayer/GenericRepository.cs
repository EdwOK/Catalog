using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Catalog.DataLayer;
using Catalog.Models;

namespace Catalog.DataAccessLayer
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : IEntity, new()
    {
        private readonly AppDbContext _dbContext;

        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(TEntity obj)
        {
            _dbContext.Connection.Insert(obj);
        }

        public TEntity GetById(int id)
        {
            return _dbContext.Connection.Get<TEntity>(entity => entity.Id == id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbContext.Connection.Table<TEntity>().ToList();
        }

        public void Update(TEntity obj)
        {
            _dbContext.Connection.Update(obj, typeof(TEntity));
        }

        public void Remove(int id)
        {
            _dbContext.Connection.Delete<TEntity>(id);
        }

        public IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Connection.Table<TEntity>().Where(predicate);
        }

        public TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Connection.Table<TEntity>().FirstOrDefault(predicate);
        }
    }
}
