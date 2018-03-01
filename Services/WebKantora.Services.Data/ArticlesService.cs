using System;
using System.Linq;
using System.Threading.Tasks;

using WebKantora.Data.Common.Contracts;
using WebKantora.Data.Models;
using WebKantora.Services.Data.Contracts;

namespace WebKantora.Services.Data
{
    public class ArticlesService : IArticlesService
    {
        private IArticleDbRepository articles;
        private IUnitOfWork unitOfWork;

        public ArticlesService(IArticleDbRepository articles, IUnitOfWork unitOfWork)
        {
            this.articles = articles;
            this.unitOfWork = unitOfWork;
        }

        public async Task Add(Article article)
        {
            await this.articles.Add(article);
            await this.unitOfWork.Commit();
        }

        public IQueryable GetAll()
        {
            var allArticles = this.articles.All()
                .OrderByDescending(a => a.Date);

            return allArticles;
        }

        public async Task<Article> GetById(Guid id)
        {
            var article = await this.articles.GetById(id);
            return article;
        }
    }
}
