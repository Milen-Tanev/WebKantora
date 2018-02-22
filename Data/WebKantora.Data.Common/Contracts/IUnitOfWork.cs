using System.Threading.Tasks;

namespace WebKantora.Data.Common.Contracts
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
