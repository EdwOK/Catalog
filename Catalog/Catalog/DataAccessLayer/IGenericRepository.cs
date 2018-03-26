using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Catalog.Models;

namespace Catalog.DataAccessLayer
{
    public interface IGenericRepository<TEntity> where TEntity : IEntity
    {
        void Add(TEntity obj, bool recursive = false);

        TEntity GetById(int id, bool recursive = false);

        IEnumerable<TEntity> GetAll();

        void Update(TEntity obj);

        void Remove(TEntity obj, bool recursive = false);

        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate);

        TEntity Find(Expression<Func<TEntity, bool>> predicate);
    }
}
