using System.Threading.Tasks;
using WebKantora.Data.Models;

namespace WebKantora.Services.Data.Contracts
{
    public interface IMessagesService
    {
        Task Add(Message message);
    }
}
