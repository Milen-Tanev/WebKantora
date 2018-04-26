using System;
using System.Linq;
using System.Threading.Tasks;

using WebKantora.Data.Models;

namespace WebKantora.Data.Common.Contracts
{
    public interface IUserDbRepository
    {
        Task Add(User entity);

        IQueryable<User> All();

        Task<User> GetById(Guid id);

        Task Delete(Guid id);

        Task<User> GetByUserName(string userName);

        void Update(Guid id, User entity);
    }
}
