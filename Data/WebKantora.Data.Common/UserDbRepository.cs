using Microsoft.EntityFrameworkCore;
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
            return this.DbSet.AsNoTracking();
        }

        public async Task Delete(string id)
        {
            var entity = await GetById(id);
            entity.IsDeleted = true;
        }

        public async Task<User> GetById(string id)
        {
            return await this.DbSet
                .Include(e => e.Messages)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public void Update(string id, User entity)
        {
            this.DbSet.Update(entity);
        }
    }
}
