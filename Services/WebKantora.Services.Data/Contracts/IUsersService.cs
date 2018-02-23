using System.Threading.Tasks;
using WebKantora.Data.Models;

namespace WebKantora.Services.Data.Contracts
{
    public interface IUsersService
    {
        Task<User> GetByUserName(string userName);
    }
}
