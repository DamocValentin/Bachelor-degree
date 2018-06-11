using Data.Core.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Core.Interfaces
{
    public interface IRatingRepository : IGenericRepository<Rating>
    {
        Task<List<Rating>> GetUsersRankingByPageAsync(int pageIndex, int pageSize, string typeOfRating);
        Task<Rating> GetRatingByUserIdAsync(Guid id);
    }
}
