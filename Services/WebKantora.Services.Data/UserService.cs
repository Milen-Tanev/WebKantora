using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WebKantora.Data.Common.Contracts;
using WebKantora.Data.Models;
using WebKantora.Services.Data.Contracts;

namespace WebKantora.Services.Data
{
    public class UserService : IUserService
    {
        private IWebKantoraDbRepository<User> users;
        private IUnitOfWork unitOfWork;

        public UserService(IWebKantoraDbRepository<User> users, IUnitOfWork unitOfWork)
        {
            this.users = users ?? throw new ArgumentNullException("The users Db repository cannot be null.");
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException("The unit of work cannot be null.");
        }

        public async Task<User> GetById(Guid id)
        {
            return await this.users.GetById(id)
                .Include(u => u.Messages)
                .FirstOrDefaultAsync();
        }

        public async Task<User> GetByUserName(string userName)
        {
            return await this.users.GetAll().FirstOrDefaultAsync(u => u.UserName == userName);
        }
    }
}
