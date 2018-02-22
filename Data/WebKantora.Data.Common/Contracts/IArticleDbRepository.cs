using System;
using System.Linq;
using System.Threading.Tasks;

using WebKantora.Data.Models;

namespace WebKantora.Data.Common.Contracts
{
    public interface IArticleDbRepository
    {
        Task Add(Article entity);

        IQueryable<Article> All();

        Task<Article> GetById(Guid id);

        Task Delete(Guid id);

        void Update(Guid id, Article entity);
    }
}
