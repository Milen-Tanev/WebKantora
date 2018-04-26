using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

using WebKantora.Data.Common.Contracts;
using WebKantora.Data.Models;
using WebKantora.Services.Data.Contracts;

namespace WebKantora.Services.Data
{
    public class ArticleService : IArticleService
    {
        private IWebKantoraDbRepository<Article> articles;
        private IUnitOfWork unitOfWork;

        public ArticleService(IWebKantoraDbRepository<Article> articles, IUnitOfWork unitOfWork)
        {
            this.articles = articles ?? throw new ArgumentNullException("The articles Db repository cannot be null.");
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException("The unit of work cannot be null.");
        }


        public async Task<Article> GetById(Guid id)
        {
            var article = await this.articles.GetById(id)
                .Include(e => e.Author)
                .Include(e => e.KeywordArticles)
                    .ThenInclude(keywordArticles => keywordArticles.Keyword)
                .FirstOrDefaultAsync();
            return article;
        }

        public IQueryable<Article> GetAll()
        {
            var allArticles = this.articles.GetAll()
                .OrderByDescending(a => a.Date)
                .Include(e => e.Author)
                .Include(e => e.KeywordArticles)
                    .ThenInclude(keywordArticles => keywordArticles.Keyword);

            return allArticles;
        }

        public IQueryable<Article> GetByKeyword(Guid keywordId)
        {
            var articles = this.articles.GetAll()
                .Where(a => a.KeywordArticles.Any(x => x.KeywordId == keywordId))
                .Include(e => e.Author)
                .Include(e => e.KeywordArticles)
                    .ThenInclude(keywordArticles => keywordArticles.Keyword);

            return articles;
        }

        public async Task Add(Article article)
        {
            await this.articles.Add(article);
            await this.unitOfWork.Commit();
        }

        public async Task Delete(Guid id)
        {
            await this.articles.Delete(id);
            await this.unitOfWork.Commit();
        }
    }
}
