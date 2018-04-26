using System.Collections.Generic;
using System.Threading.Tasks;

using WebKantora.Data.Common.Contracts;
using WebKantora.Data.Models;
using WebKantora.Services.Data.Contracts;

namespace WebKantora.Services.Data
{
    public class KeywordArticlesService : IKeywordArticlesService
    {
        private IWebKantoraDbRepository<KeywordArticle> keywordArticles;
        private IUnitOfWork unitOfWork;

        public KeywordArticlesService(IKeywordArticleDbRepository keywordArticles, IUnitOfWork unitOfWork)
        {
            this.keywordArticles = keywordArticles;
            this.unitOfWork = unitOfWork;
        }

        public async Task Add(ICollection<KeywordArticle> collection)
        {
            await this.keywordArticles.Add(collection);
            await this.unitOfWork.Commit();
        }
    }
}
