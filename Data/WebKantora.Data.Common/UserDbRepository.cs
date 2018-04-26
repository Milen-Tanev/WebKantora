using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

using WebKantora.Data.Common.Contracts;
using WebKantora.Data.Models;

namespace WebKantora.Data.Common
{
    public class UserDbRepository : IUserDbRepository
    {
        public UserDbRepository(WebKantoraDbContext context)
        {
            this.Context = context;
            this.DbSet = this.Context.Set<User>();
        }

        public WebKantoraDbContext Context { get; }

        public DbSet<User> DbSet { get; }

        public async Task Add(User entity)
        {
            await this.DbSet.AddAsync(entity);
        }

        public IQueryable<User> All()
        {
            return this.DbSet.AsQueryable()
                .Where(x => !x.IsDeleted);
        }

        public async Task Delete(Guid id)
        {
            var entity = await this.GetById(id);
            entity.IsDeleted = true;
        }

        public async Task<User> GetById(Guid id)
        {
            return await this.DbSet
                .Where(x => !x.IsDeleted)
                .Include(e => e.Messages)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<User> GetByUserName(string userName)
        {
            return await this.DbSet
                .Where(x => !x.IsDeleted)
                .Include(e => e.Messages)
                .FirstOrDefaultAsync(e => e.UserName == userName);
        }

        public void Update(Guid id, User entity)
        {
            this.DbSet.Update(entity);
        }
    }
}
