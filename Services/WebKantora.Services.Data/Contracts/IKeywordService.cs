using System;
using System.Linq;
using System.Threading.Tasks;

using WebKantora.Data.Models;

namespace WebKantora.Services.Data.Contracts
{
    public interface IKeywordService
    {
        Task Add(Keyword keyword);

        IQueryable<Keyword> GetAll();

        Task Update(Guid Id, Keyword entity);
    }
}
