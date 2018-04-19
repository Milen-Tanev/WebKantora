using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

using WebKantora.Data.Common.Contracts;
using WebKantora.Data.Models;

namespace WebKantora.Data.Common
{
    public class ArticleDbRepository : DbRepository<Article>,  IArticleDbRepository
    {
        public ArticleDbRepository(WebKantoraDbContext context)
            :base(context)
        {
        }

        protected override IQueryable<Article> Include(IQueryable<Article> entity)
        {
            return entity
                .Include(e => e.Author)
                .Include(e => e.KeywordArticles)
                    .ThenInclude(keywordArticles => keywordArticles.Keyword);
        }
    }
}
