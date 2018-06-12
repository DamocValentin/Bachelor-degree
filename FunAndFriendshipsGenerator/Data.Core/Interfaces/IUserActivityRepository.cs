using Data.Core.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Core.Interfaces
{
    public interface IUserActivityRepository : IGenericRepository<UserActivity>
    {
        Task<List<User>> GetAllUsersByActivityIdAsync(Guid activityId);
        Task<User> GetOwnerByActivityIdAsync(Guid activityId);
        Task<UserActivity> GetUserActivityByUserAndActivityAsync(Guid userId, Guid activityId);
    }
}
