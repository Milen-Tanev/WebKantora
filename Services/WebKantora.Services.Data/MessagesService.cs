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
        private ICustomErrorService customErrors;

        public MessagesService(IMessageDbRepository messages, IUnitOfWork unitOfWork, ICustomErrorService customErrors)
        {
            this.messages = messages;
            this.unitOfWork = unitOfWork;
            this.customErrors = customErrors;
        }

        public async Task Add(Message message)
        {
            //try
            //{
            //    await this.messages.Add(message);
            //    await this.unitOfWork.Commit();
            //}
            //catch (Exception ex)
            //{
            //    var error = new CustomError()
            //    {
            //        InnerException = "",
            //        Message = ex.Message,
            //        Source = ex.Source,
            //        StackTrace = ex.StackTrace,
            //        CustomMessage = ""
            //    };

            //    await this.customErrors.Add(error);
            //}
            await this.messages.Add(message);
            await this.unitOfWork.Commit();
        }
    }
}
