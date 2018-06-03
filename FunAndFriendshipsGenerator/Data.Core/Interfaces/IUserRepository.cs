using Data.Core.Domain;
using System.Threading.Tasks;

namespace Data.Core.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<string> GetUsernameByEmailAsync(string emailAdress);
    }
}
