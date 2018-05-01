using Business.Repository;
using Data.Core;
using Data.Core.Interfaces;
using Data.Persistance;

namespace Business
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context; public IActivityRepository Activities { get; private set; }
        public IActivityTypeRepository ActivityTypes { get; private set; }
        public IRatingRepository Ratings { get; private set; }
        public IReviewRepository Reviews { get; private set; }
        public IUserRepository Users { get; private set; }
        public IUserActivityRepository UserActivities { get; private set; }

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
            Activities = new ActivityRepository(_context);
            ActivityTypes = new ActivityTypeRepository(_context);
            Ratings = new RatingRepository(_context);
            Reviews = new ReviewRepository(_context);
            Users = new UserRepository(_context);
            UserActivities = new UserActivityRepository(_context);

        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}