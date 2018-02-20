using System.Threading.Tasks;

using MimeKit;

namespace WebKantora.Services.Web.Contracts
{
    public interface IEmailSenderService
    {
        bool SendEmailForUserRequest(MimeMessage mimeMessage);
    }
}
