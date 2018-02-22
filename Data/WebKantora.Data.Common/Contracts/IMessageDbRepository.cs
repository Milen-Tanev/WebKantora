using System;
using System.Linq;
using System.Threading.Tasks;

using WebKantora.Data.Models;

namespace WebKantora.Data.Common.Contracts
{
    public interface IMessageDbRepository
    {
        Task Add(Message entity);

        IQueryable<Message> All();

        Task<Message> GetById(Guid id);

        Task Delete(Guid id);

        void Update(Guid id, Message entity);
    }
}
