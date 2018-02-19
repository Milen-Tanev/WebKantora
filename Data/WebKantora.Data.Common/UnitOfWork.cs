using WebKantora.Data.Common.Contracts;

namespace WebKantora.Data.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WebKantoraDbContext context;

        public UnitOfWork(WebKantoraDbContext context)
        {
            this.context = context;
        }

        public void Commit()
        {
            this.context.SaveChanges();
        }
    }
}
