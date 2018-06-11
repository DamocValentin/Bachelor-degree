using Data.Core.Interfaces;
using Data.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _entities;

        public GenericRepository(DatabaseContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id) => await _entities.FindAsync(id);

        public virtual async Task<List<TEntity>> GetAllAsync() => await _entities.ToListAsync();

        public virtual async Task<bool> InsertAsync(TEntity entity)
        {
            _entities.Add(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public virtual async Task<bool> UpdateAsync(TEntity entity)
        {
            _entities.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public virtual async Task<bool> DeleteAsync(TEntity entity)
        {
            _entities.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
