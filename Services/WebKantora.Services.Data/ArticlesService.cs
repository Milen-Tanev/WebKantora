using System;
using System.Linq;
using WebKantora.Data.Common.Contracts;
using WebKantora.Data.Models;
using WebKantora.Services.Data.Contracts;

namespace WebKantora.Services.Data
{
    public class ArticlesService : IArticlesService
    {
        private IWebKantoraDbRepository<Article> articles;
        private IUnitOfWork unitOfWork;

        public ArticlesService(IWebKantoraDbRepository<Article> articles, IUnitOfWork unitOfWork)
        {
            this.articles = articles;
            this.unitOfWork = unitOfWork;
        }

        public void Add(Article article)
        {
            this.articles.Add(article);
            this.unitOfWork.Commit();
        }

        public IQueryable GetAll()
        {
            var allArticles = this.articles.All()
                .OrderByDescending(a => a.Date);

            return allArticles;
        }

        public Article GetById(Guid id)
        {
            var article = this.articles.GetById(id);
            return article;
        }
    }
}
