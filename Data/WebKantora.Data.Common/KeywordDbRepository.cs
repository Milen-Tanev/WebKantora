using Microsoft.EntityFrameworkCore;
using System.Linq;

using WebKantora.Data.Common.Contracts;
using WebKantora.Data.Models;

namespace WebKantora.Data.Common
{
    public class KeywordDbRepository : DbRepository<Keyword>, IKeywordDbRepository
    {
        public KeywordDbRepository(WebKantoraDbContext context)
            : base(context)
        {
        }

        protected override IQueryable<Keyword> Include(IQueryable<Keyword> entity)
        {
            return entity
                .Include(e => e.KeywordArticles);
        }
    }
}
