using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Catalog.DataLayer;
using Catalog.Models;
using SQLiteNetExtensions.Extensions;

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

        public void Add(TEntity obj, bool recursive = false)
        {
            _dbContext.Connection.InsertWithChildren(obj, recursive);
            // _dbContext.Connection.Insert(obj);
        }

        public TEntity GetById(int id, bool recursive = false)
        {
            return _dbContext.Connection.GetWithChildren<TEntity>(id, recursive);
            //return _dbContext.Connection.Get<TEntity>(entity => entity.Id == id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbContext.Connection.GetAllWithChildren<TEntity>().ToList();
            //return _dbContext.Connection.Table<TEntity>().ToList();
        }

        public void Update(TEntity obj)
        {
            //_dbContext.Connection.Update(obj, typeof(TEntity));
            _dbContext.Connection.UpdateWithChildren(obj);
        }

        public void Remove(TEntity obj, bool recursive = false)
        {
            _dbContext.Connection.Delete(obj, recursive);
            //_dbContext.Connection.Delete<TEntity>(id);
        }

        public IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Connection.GetAllWithChildren(predicate);
            //return _dbContext.Connection.Table<TEntity>().Where(predicate);
        }

        public TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Connection.GetWithChildren<TEntity>(predicate);
            //return _dbContext.Connection.Table<TEntity>().FirstOrDefault(predicate);
        }
    }
}
