using System;
using System.Linq;
using System.Threading.Tasks;
using WebKantora.Data.Models.Contracts;

namespace WebKantora.Data.Common.Contracts
{
    public interface IWebKantoraDbRepository<T>
        where T : class, IEntity, IDeletable
    {
        IQueryable<T> GetById(Guid id);

        IQueryable<T> GetAll();

        Task Add(T entity);

        Task Delete(T entity);

        Task Delete(Guid id);

        Task Update(T entity);

        Task Update(Guid id);
    }
}
