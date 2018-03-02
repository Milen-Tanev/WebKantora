using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

using WebKantora.Data.Common.Contracts;
using WebKantora.Data.Models;

namespace WebKantora.Data.Common
{
    public class MessageDbRepository : IMessageDbRepository
    {
        public MessageDbRepository(WebKantoraDbContext context)
        {
            this.Context = context;
            this.DbSet = this.Context.Set<Message>();
        }

        public WebKantoraDbContext Context { get; }

        public DbSet<Message> DbSet { get; }

        public async Task Add(Message entity)
        {
            await this.DbSet.AddAsync(entity);
        }

        public IQueryable<Message> All()
        {
            return this.DbSet.AsNoTracking();
        }

        public async Task Delete(Guid id)
        {
            var entity = await this.GetById(id);
            entity.IsDeleted = true;
        }

        public async Task<Message> GetById(Guid id)
        {
            return await this.DbSet
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public void Update(Guid id, Message entity)
        {
            this.DbSet.Update(entity);
        }
    }
}
