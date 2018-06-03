using Data.Core.Domain;
using Data.Core.Interfaces;
using Data.Persistance;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DatabaseContext context) : base(context)
        {
        }

        public DatabaseContext DatabaseContext
        {
            get { return _context as DatabaseContext; }
        }

        public async Task<string> GetUsernameByEmailAsync(string emailAdress)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.Equals(emailAdress));
            return user.UserName;
        }
    }
}
