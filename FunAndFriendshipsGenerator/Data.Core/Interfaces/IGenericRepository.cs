using System;
using System.Collections.Generic;

namespace Data.Core.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        TEntity GetById(Guid id);
        List<TEntity> GetAll();

        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
