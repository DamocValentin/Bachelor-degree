using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Core.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(Guid id);
        Task<List<TEntity>> GetAllAsync();
        Task<bool> InsertAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(TEntity entity);
    }
}
