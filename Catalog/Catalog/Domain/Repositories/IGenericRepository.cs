using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Catalog.Domain.Entities;

namespace Catalog.Domain.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : Entity
    {
        void Add(TEntity obj);

        TEntity GetById(int id);

        IEnumerable<TEntity> GetAll();

        void Update(TEntity obj);

        void Remove(int id);

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    }
}
