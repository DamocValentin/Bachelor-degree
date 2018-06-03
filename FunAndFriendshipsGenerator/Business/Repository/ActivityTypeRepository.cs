using Data.Core.Domain;
using Data.Core.Interfaces;
using Data.Persistance;

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
    }
}
