using Data.Core.Domain;
using Data.Core.Interfaces;
using Data.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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
            var user = await DatabaseContext.Users.FirstOrDefaultAsync(x => x.Email.Equals(emailAdress));
            return user.UserName;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await DatabaseContext.Users.FirstOrDefaultAsync(x => x.UserName.Equals(username));
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await DatabaseContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
