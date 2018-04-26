using System;
using System.Threading.Tasks;

using WebKantora.Data.Common.Contracts;
using WebKantora.Data.Models;
using WebKantora.Services.Data.Contracts;

namespace WebKantora.Services.Data
{
    public class CustomErrorService : ICustomErrorService
    {
        private IWebKantoraDbRepository<CustomError> customErrors;
        private IUnitOfWork unitOfWork;

        public CustomErrorService(IWebKantoraDbRepository<CustomError> customErrors, IUnitOfWork unitOfWork)
        {
            this.customErrors = customErrors ?? throw new ArgumentNullException("The custom errors Db repository cannot be null.");
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException("The unit of work cannot be null.");
        }

        public async Task Add(CustomError customError)
        {
            try
            {
                await this.customErrors.Add(customError);
                await this.unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
