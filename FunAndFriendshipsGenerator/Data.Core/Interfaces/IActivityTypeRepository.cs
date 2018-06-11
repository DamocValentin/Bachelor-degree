using Data.Core.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Core.Interfaces
{
    public interface IActivityTypeRepository : IGenericRepository<ActivityType>
    {
        Task<Guid> GetActivityIdByNameAsync(string name);
    }
}
