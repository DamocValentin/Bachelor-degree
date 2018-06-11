using Data.Core.Domain;
using System;
using System.Threading.Tasks;

namespace Data.Core.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<string> GetUsernameByEmailAsync(string emailAdress);
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> GetUserByIdAsync(Guid id);
    }
}
