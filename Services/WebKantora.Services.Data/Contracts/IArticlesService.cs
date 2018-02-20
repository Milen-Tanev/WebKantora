using System;
using System.Linq;

using WebKantora.Data.Models;

namespace WebKantora.Services.Data.Contracts
{
    public interface IArticlesService
    {
        void Add(Article article);

        IQueryable GetAll();

        Article GetById(Guid id);
    }
}
