using Data.Core.Domain;
using Data.Core.Interfaces;
using Data.Persistance;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;

namespace Business.Repository
{
    public class ActivityTypeRepository : GenericRepository<ActivityType>, IActivityTypeRepository
    {
        public ActivityTypeRepository(DatabaseContext context) : base(context)
        {
        }

        public DatabaseContext DatabaseContext
        {
            get { return _context as DatabaseContext; }
        }

        public async Task<Guid> GetActivityIdByNameAsync(string name)
        {
            var activityType = await DatabaseContext.ActivityTypes.FirstOrDefaultAsync(x => x.ActivityTypeName.Equals(name));
            return activityType.Id;
        }
    }
}
