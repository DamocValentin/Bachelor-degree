using Data.Core.Domain;
using Data.Core.Interfaces;
using Data.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        public ReviewRepository(DatabaseContext context) : base(context)
        {
        }

        public DatabaseContext DatabaseContext
        {
            get { return _context as DatabaseContext; }
        }

        public async Task<Review> GetReviewByUserAndActivityAsync(Guid userId, Guid activityId)
        {
            return await DatabaseContext.Reviews.FirstOrDefaultAsync(x => x.ActivityId == activityId && x.UserId == userId);
        }
    }
}
