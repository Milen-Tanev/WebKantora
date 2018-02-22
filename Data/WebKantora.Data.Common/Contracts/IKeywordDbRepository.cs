using System;
using System.Linq;
using System.Threading.Tasks;

using WebKantora.Data.Models;

namespace WebKantora.Data.Common.Contracts
{
    public interface IKeywordDbRepository
    {
        Task Add(Keyword entity);

        IQueryable<Keyword> All();

        Task<Keyword> GetById(Guid id);

        Task Delete(Guid id);

        void Update(Guid id, Keyword entity);
    }
}
