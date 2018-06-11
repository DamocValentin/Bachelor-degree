using Data.Core.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Core.Interfaces
{
    public interface IActivityRepository : IGenericRepository<Activity>
    {
        Task<List<Activity>> GetAllAvailableActivitiesByTypeIdAsync(Guid id);
    }
}
