using System;
using System.Linq;
using System.Threading.Tasks;
using WebKantora.Data.Models;

namespace WebKantora.Services.Data.Contracts
{
    public interface IArticleService
    {
        Task Add(Article article);

        IQueryable<Article> GetAll();

        Task<Article> GetById(Guid id);

        IQueryable<Article> GetByKeyword(Guid keywordId);

        Task Delete(Guid id);
    }
}
