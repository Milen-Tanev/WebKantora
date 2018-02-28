using System;
using System.Linq;
using System.Threading.Tasks;

using WebKantora.Data.Models;

namespace WebKantora.Data.Common.Contracts
{
    public interface ICustomErrorDbRepository
    {
        Task Add(CustomError entity);

        IQueryable<CustomError> All();

        Task<CustomError> GetById(Guid id);

        Task Delete(Guid id);

        void Update(Guid id, CustomError entity);
    }
}
