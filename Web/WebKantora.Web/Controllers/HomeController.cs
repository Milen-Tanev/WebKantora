using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using MailKit;

using WebKantora.Services.Web.Contracts;
using WebKantora.Web.Models.ContactViewModels;
using MimeKit;

namespace WebKantora.Web.Controllers
{
    public class HomeController : Controller
    {
        IEmailSenderService emailSenderService;

        public HomeController(IEmailSenderService emailSenderService)
        {
            this.emailSenderService = emailSenderService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Blog()
        {
            return View();
        }

        public IActionResult Payment()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                //TODO: make partial views
                string subject = "Запитване ," + model.FirstName + " " + model.LastName;
                string message = model.FirstName + " " + model.LastName + "//n" + model.Email + " ," + model.PhoneNumber + "//n" + model.Message;
                var mimeMessage = new MimeMessage();

                mimeMessage.Subject = subject;
                mimeMessage.Body = new TextPart("plain")
                {
                    Text = message
                };
                this.emailSenderService.SendEmailForUserRequestAsync(mimeMessage);
                return this.RedirectToAction("Index", "Home");
            }
            return this.View(model);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
