using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

using WebKantora.Data.Common.Contracts;
using WebKantora.Data.Models;

namespace WebKantora.Data.Common
{
    public class MessageDbRepository : DbRepository<Message>, IMessageDbRepository
    {
        public MessageDbRepository(WebKantoraDbContext context)
            : base(context)
        {
        }

        protected override IQueryable<Message> Include(IQueryable<Message> entity)
        {
            return entity;
        }
    }
}
