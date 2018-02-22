using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

using WebKantora.Data.Common.Contracts;
using WebKantora.Data.Models;

namespace WebKantora.Data.Common
{
    public class KeywordDbRepository : IKeywordDbRepository
    {
        public KeywordDbRepository(WebKantoraDbContext context)
        {
            this.Context = context;
            this.DbSet = this.Context.Set<Keyword>();
        }

        public WebKantoraDbContext Context { get; }

        public DbSet<Keyword> DbSet { get; }

        public async Task Add(Keyword entity)
        {
            await this.DbSet.AddAsync(entity);
        }

        public IQueryable<Keyword> All()
        {
            return this.DbSet.AsNoTracking();
        }

        public async Task Delete(Guid id)
        {
            var entity = await GetById(id);
            entity.IsDeleted = true;
        }

        public async Task<Keyword> GetById(Guid id)
        {
            return await this.DbSet
                .Include(e => e.KeywordArticles)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public void Update(Guid id, Keyword entity)
        {
            this.DbSet.Update(entity);
        }
    }
}
