using Data.Core.Domain;
using Data.Core.Interfaces;
using Data.Persistance;

namespace Business.Repository
{
    public class UserActivityRepository : GenericRepository<UserActivity>, IUserActivityRepository
    {
        public UserActivityRepository(DatabaseContext context) : base(context)
        {
        }

        public DatabaseContext DatabaseContext
        {
            get { return Context as DatabaseContext; }
        }
    }
}
