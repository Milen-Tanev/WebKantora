using System;
using System.Threading.Tasks;
using WebKantora.Data.Common.Contracts;

namespace WebKantora.Data.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WebKantoraDbContext context;

        public UnitOfWork(WebKantoraDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException("The context cannot be null.");
        }

        public async Task Commit()
        {
            try
            {
                await this.context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
