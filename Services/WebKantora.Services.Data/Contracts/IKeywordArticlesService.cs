using System.Collections.Generic;
using System.Threading.Tasks;

using WebKantora.Data.Models;

namespace WebKantora.Services.Data.Contracts
{
    public interface IKeywordArticlesService
    {
        Task Add(ICollection<KeywordArticle> collection);
    }
}
