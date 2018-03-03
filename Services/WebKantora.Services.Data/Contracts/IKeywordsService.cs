using System.Linq;
using WebKantora.Data.Models;

namespace WebKantora.Services.Data.Contracts
{
    public interface IKeywordsService
    {
        IQueryable<Keyword> GetAll();
    }
}
