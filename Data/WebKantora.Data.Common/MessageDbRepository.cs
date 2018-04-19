using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

using WebKantora.Data.Common.Contracts;
using WebKantora.Data.Models;

namespace WebKantora.Data.Common
{
    public class MessageDbRepository : DbRepository<Message>, IMessageDbRepository
    {
        public MessageDbRepository(WebKantoraDbContext context)
            : base(context)
        {
        }

        public WebKantoraDbContext Context { get; }

        public DbSet<Message> DbSet { get; }

        public async Task Add(Message entity)
        {
            await this.DbSet.AddAsync(entity);
        }

        public IQueryable<Message> All()
        {
            return this.DbSet.
                Where(x => !x.IsDeleted)
                .AsNoTracking();
        }

        protected override IQueryable<Message> Include(IQueryable<Message> entity)
        {
            return entity;
        }
    }
}
