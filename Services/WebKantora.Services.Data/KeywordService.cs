using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebKantora.Data.Common.Contracts;
using WebKantora.Data.Models;
using WebKantora.Services.Data.Contracts;

namespace WebKantora.Services.Data
{
    public class KeywordService: IKeywordService
    {
        private IWebKantoraDbRepository<Keyword> keywords;
        private IUnitOfWork unitOfWork;

        public KeywordService(IWebKantoraDbRepository<Keyword> keywords, IUnitOfWork unitOfWork)
        {
            this.keywords = keywords ?? throw new ArgumentNullException("The keywords Db repository cannot be null.");
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException("The unit of work cannot be null.");
        }

        public async Task Add(Keyword keyword)
        {
            await this.keywords.Add(keyword);
            await this.unitOfWork.Commit();
        }

        public IQueryable<Keyword> GetAll()
        {
            var allKeywords = this.keywords.GetAll()
                .OrderByDescending(a => a.Content)
                .Include(e => e.KeywordArticles);

            return allKeywords;
        }

        public async Task Update(Guid Id, Keyword entity)
        {
            await this.keywords.Update(entity);
            await this.unitOfWork.Commit();
        }
    }
}
