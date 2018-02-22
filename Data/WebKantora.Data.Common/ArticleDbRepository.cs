using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

using WebKantora.Data.Common.Contracts;
using WebKantora.Data.Models;

namespace WebKantora.Data.Common
{
    public class ArticleDbRepository : IArticleDbRepository
    {
        public ArticleDbRepository(WebKantoraDbContext context)
        {
            this.Context = context;
            this.DbSet = this.Context.Set<Article>();
        }

        public WebKantoraDbContext Context { get; }

        public DbSet<Article> DbSet { get; }

        public async Task Add(Article entity)
        {
            await this.DbSet.AddAsync(entity);
        }

        public IQueryable<Article> All()
        {
            return this.DbSet.AsNoTracking();
        }

        public async Task Delete(Guid id)
        {
            var entity = await GetById(id);
            entity.IsDeleted = true;
        }

        public async Task<Article> GetById(Guid id)
        {
            return await this.DbSet
                .Include(e => e.Author)
                .Include(e => e.KeywordArticles)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public void Update(Guid id, Article entity)
        {
            this.DbSet.Update(entity);
        }
    }
}
