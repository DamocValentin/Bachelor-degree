using Data.Core.Domain;
using Data.Core.Interfaces;
using Data.Persistance;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Business.Repository
{
    public class UserActivityRepository : GenericRepository<UserActivity>, IUserActivityRepository
    {
        public UserActivityRepository(DatabaseContext context) : base(context)
        {
        }

        public DatabaseContext DatabaseContext
        {
            get { return _context as DatabaseContext; }
        }

        public async Task<List<User>> GetAllUsersByActivityIdAsync(Guid activityId)
        {
            var userActivities = await DatabaseContext.UserActivities.ToListAsync();
            List<User> users = new List<User>();

            foreach(var userActivity in userActivities)
            {
                if( userActivity.ActivityId == activityId)
                {
                    users.Add(await DatabaseContext.Users.FirstOrDefaultAsync(x => x.Id == userActivity.UserId));
                }
            }

            return users;
        }

        public async Task<User> GetOwnerByActivityIdAsync(Guid activityId)
        {
            var userId = (await DatabaseContext.UserActivities.FirstOrDefaultAsync(x => x.ActivityId == activityId && x.Owner)).UserId;
            return await DatabaseContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<UserActivity> GetUserActivityByUserAndActivityAsync(Guid userId, Guid activityId)
        {
            return await DatabaseContext.UserActivities.FirstOrDefaultAsync(x => x.UserId == userId && x.ActivityId == activityId);
        }
    }
}
