using System.Linq;
using System.Threading.Tasks;

using WebKantora.Data.Models;

namespace WebKantora.Data.Common.Contracts
{
    public interface IUserDbRepository
    {
        Task Add(User entity);

        IQueryable<User> All();

        Task<User> GetById(string id);

        Task Delete(string id);

        void Update(string id, User entity);
    }
}
