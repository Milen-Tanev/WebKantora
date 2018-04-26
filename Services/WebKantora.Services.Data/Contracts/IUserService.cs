using System;
using System.Threading.Tasks;
using WebKantora.Data.Models;

namespace WebKantora.Services.Data.Contracts
{
    public interface IUserService
    {
        Task<User> GetById(Guid id);

        Task<User> GetByUserName(string userName);
    }
}
