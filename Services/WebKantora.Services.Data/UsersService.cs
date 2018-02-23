using System.Linq;
using System.Threading.Tasks;
using WebKantora.Data.Common.Contracts;
using WebKantora.Data.Models;
using WebKantora.Services.Data.Contracts;

namespace WebKantora.Services.Data
{
    public class UsersService : IUsersService
    {
        private IUserDbRepository users;
        private IUnitOfWork unitOfWork;

        public UsersService(IUserDbRepository users, IUnitOfWork unitOfWork)
        {
            this.users = users;
            this.unitOfWork = unitOfWork;
        }

        public async Task<User> GetByUserName(string userName)
        {
            var user = await this.users.GetByUserName(userName);
            return user;
        }
    }
}
