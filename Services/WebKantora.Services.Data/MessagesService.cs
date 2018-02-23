using System;
using System.Threading.Tasks;
using WebKantora.Data.Common.Contracts;
using WebKantora.Data.Models;
using WebKantora.Services.Data.Contracts;

namespace WebKantora.Services.Data
{
    public class MessagesService : IMessagesService
    {
        private IMessageDbRepository messages;
        private IUnitOfWork unitOfWork;

        public MessagesService(IMessageDbRepository messages, IUnitOfWork unitOfWork)
        {
            this.messages = messages;
            this.unitOfWork = unitOfWork;
        }

        public async Task Add(Message message)
        {
            try
            {
                await this.messages.Add(message);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            await this.unitOfWork.Commit();
        }
    }
}
