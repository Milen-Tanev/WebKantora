﻿using System.Threading.Tasks;

using MimeKit;

namespace WebKantora.Services.Web.Contracts
{
    public interface IEmailSenderService
    {
        Task SendEmailForUserRequestAsync(MimeMessage mimeMessage);
    }
}