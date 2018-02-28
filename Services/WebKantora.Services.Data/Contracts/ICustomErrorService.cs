using System.Threading.Tasks;

using WebKantora.Data.Models;

namespace WebKantora.Services.Data.Contracts
{
    public interface ICustomErrorService
    {
        Task Add(CustomError message);
    }
}
