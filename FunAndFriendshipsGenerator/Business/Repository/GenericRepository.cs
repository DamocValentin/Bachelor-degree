using Data.Core.Interfaces;
using Data.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly DatabaseContext Context;
        protected readonly DbSet<TEntity> _entities;

        public GenericRepository(DatabaseContext context)
        {
            Context = context;
            _entities = context.Set<TEntity>();
        }

        public virtual TEntity GetById(Guid id) => _entities.Find(id);
        public virtual List<TEntity> GetAll() => _entities.ToList();
        public virtual void Insert(TEntity entity) => _entities.Add(entity);
        public virtual void Update(TEntity entity) => _entities.Update(entity);
        public virtual void Delete(TEntity entity) => _entities.Remove(entity);
    }
}
