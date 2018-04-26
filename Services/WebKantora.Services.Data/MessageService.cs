using System;
using System.Threading.Tasks;
using WebKantora.Data.Common.Contracts;
using WebKantora.Data.Models;
using WebKantora.Services.Data.Contracts;

namespace WebKantora.Services.Data
{
    public class MessageService : IMessageService
    {
        private IWebKantoraDbRepository<Message> messages;
        private IUnitOfWork unitOfWork;

        public MessageService(IWebKantoraDbRepository<Message> messages, IUnitOfWork unitOfWork)
        {
            this.messages = messages ?? throw new ArgumentNullException("The messages Db repository cannot be null.");
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException("The unit of work cannot be null.");
        }

        public async Task Add(Message message)
        {
            await this.messages.Add(message);
            await this.unitOfWork.Commit();
        }
    }
}
