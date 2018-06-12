using Data.Core.Domain;
using System;
using System.Threading.Tasks;

namespace Data.Core.Interfaces
{
    public interface IReviewRepository : IGenericRepository<Review>
    {
        Task<Review> GetReviewByUserAndActivityAsync(Guid userId, Guid activityId);
    }
}
