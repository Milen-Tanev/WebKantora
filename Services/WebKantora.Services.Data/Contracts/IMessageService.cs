using System.Threading.Tasks;
using WebKantora.Data.Models;

namespace WebKantora.Services.Data.Contracts
{
    public interface IMessageService
    {
        Task Add(Message message);
    }
}
