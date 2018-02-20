using System;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;

using WebKantora.Services.Web.Contracts;

namespace WebKantora.Services.Web
{
    public class EmailSenderService : IEmailSenderService
    {
        //TODO: make async
        public bool SendEmailForUserRequest(MimeMessage mimeMessage)
        {
            bool result = false;
            //TODO: extract constants
            //TODO: get password from database
            try
            {
                string email = "webkantoratest@gmail.com";
                string password = "password-1";

                string SmtpServer = "smtp.gmail.com";
                int SmtpPort = 587;

                mimeMessage.From.Add(new MailboxAddress(email));
                mimeMessage.To.Add(new MailboxAddress(email));

                using (var client = new SmtpClient())
                {
                    client.Connect(SmtpServer, SmtpPort, false);
                    client.Authenticate(email, password);
                    client.Send(mimeMessage);
                    client.Disconnect(true);
                }
                result = true;
            }
            catch (Exception ex)
            {
                string Msg = ex.Message;
                result = false;
            }

            return result;
        }
    }
}
