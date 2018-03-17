using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

using WebKantora.Data.Common.Contracts;
using WebKantora.Data.Models;

namespace WebKantora.Data.Common
{
    public class KeywordArticleDbRepository : IKeywordArticleDbRepository
    {
        public KeywordArticleDbRepository(WebKantoraDbContext context)
        {
            this.Context = context;
            this.DbSet = this.Context.Set<KeywordArticle>();
        }

        public WebKantoraDbContext Context { get; }

        public DbSet<KeywordArticle> DbSet { get; }

        public async Task Add(ICollection<KeywordArticle> collection)
        {
            await this.DbSet.AddRangeAsync(collection);
        }
    }
}
