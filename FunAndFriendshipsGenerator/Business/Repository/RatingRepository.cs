using Data.Core.Domain;
using Data.Core.Interfaces;
using Data.Persistance;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class RatingRepository : GenericRepository<Rating>, IRatingRepository
    {
        public RatingRepository(DatabaseContext context) : base(context)
        {
        }

        public DatabaseContext DatabaseContext
        {
            get { return _context as DatabaseContext; }
        }

        public async Task<List<Rating>> GetUsersRankingByPageAsync(int pageIndex, int pageSize, string typeOfRating)
        {
            if (typeOfRating.Equals("SkillScore"))
            {
                return await DatabaseContext.Ratings.OrderBy(x => x.SkillScore).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            }

            return await DatabaseContext.Ratings.OrderBy(x => x.BehaviourScore).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        }
    }
}
