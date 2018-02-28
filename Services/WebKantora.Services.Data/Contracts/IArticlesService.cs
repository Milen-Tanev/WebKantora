using System;
using System.Linq;
using System.Threading.Tasks;
using WebKantora.Data.Models;

namespace WebKantora.Services.Data.Contracts
{
    public interface IArticlesService
    {
        Task Add(Article article);

        IQueryable GetAll();

        Task<Article> GetById(Guid id);
    }
}
