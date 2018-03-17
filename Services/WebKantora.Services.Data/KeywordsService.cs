using System;
using System.Linq;
using System.Threading.Tasks;
using WebKantora.Data.Common.Contracts;
using WebKantora.Data.Models;
using WebKantora.Services.Data.Contracts;

namespace WebKantora.Services.Data
{
    public class KeywordsService: IKeywordsService
    {
        private IKeywordDbRepository keywords;
        private IUnitOfWork unitOfWork;

        public KeywordsService(IKeywordDbRepository keywords, IUnitOfWork unitOfWork)
        {
            this.keywords = keywords;
            this.unitOfWork = unitOfWork;
        }

        public async Task Add(Keyword keyword)
        {
            await this.keywords.Add(keyword);
            await this.unitOfWork.Commit();
        }

        public IQueryable<Keyword> GetAll()
        {
            var allKeywords = this.keywords.All()
                .OrderByDescending(a => a.Content);

            return allKeywords;
        }

        public async Task Update(Guid Id, Keyword entity)
        {
            this.keywords.Update(Id, entity);
            await this.unitOfWork.Commit();
        }
    }
}
