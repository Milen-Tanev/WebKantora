using WebKantora.Data.Models;

namespace WebKantora.Services.Data.Contracts
{
    public interface IUsersService
    {
        User GetByUserName(string userName);
    }
}
