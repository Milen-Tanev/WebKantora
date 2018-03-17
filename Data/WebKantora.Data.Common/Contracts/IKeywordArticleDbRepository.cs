using System.Collections.Generic;
using System.Threading.Tasks;

using WebKantora.Data.Models;

namespace WebKantora.Data.Common.Contracts
{
    public interface IKeywordArticleDbRepository
    {
        Task Add(ICollection<KeywordArticle> collection);
    }
}
