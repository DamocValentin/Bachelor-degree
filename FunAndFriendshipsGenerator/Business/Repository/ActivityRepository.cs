using Data.Core.Domain;
using Data.Core.Interfaces;
using Data.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class ActivityRepository : GenericRepository<Activity>, IActivityRepository
    {
        public ActivityRepository(DatabaseContext context) : base(context)
        {
        }

        public DatabaseContext DatabaseContext
        {
            get { return _context as DatabaseContext; }
        }

        public async Task<List<Activity>> GetAllAvailableActivitiesByTypeIdAsync(Guid id)
        {
            List<Activity> activities = new List<Activity>();
            var allActivities = await DatabaseContext.Activities.ToListAsync();

            foreach(var activity in allActivities)
            {
                if(activity.StartTime > DateTime.Now && activity.MaxParticipantsNumber > activity.ParticipantsNumber && activity.ActivityTypeId == id)
                {
                    activities.Add(activity);
                }
            }

            return activities;
        }
    }
}
