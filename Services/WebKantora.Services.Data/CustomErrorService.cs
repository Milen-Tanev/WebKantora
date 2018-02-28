using System;
using System.Threading.Tasks;

using WebKantora.Data.Common.Contracts;
using WebKantora.Data.Models;
using WebKantora.Services.Data.Contracts;

namespace WebKantora.Services.Data
{
    public class CustomErrorService : ICustomErrorService
    {
        private ICustomErrorDbRepository customErrors;
        private IUnitOfWork unitOfWork;

        public CustomErrorService(ICustomErrorDbRepository customErrors, IUnitOfWork unitOfWork)
        {
            this.customErrors = customErrors;
            this.unitOfWork = unitOfWork;
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
