using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using MailKit;

using WebKantora.Services.Web.Contracts;
using WebKantora.Web.Models.ContactViewModels;
using MimeKit;
using Microsoft.AspNetCore.Identity;
using WebKantora.Services.Data.Contracts;

namespace WebKantora.Web.Controllers
{
    public class HomeController : Controller
    {
        private IEmailSenderService emailSenderService;
        private IUsersService usersService;

        public HomeController(IEmailSenderService emailSenderService, IUsersService usersService)
        {
            this.emailSenderService = emailSenderService;
            this.usersService = usersService;
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
            var model = new ContactFormViewModel();

            if (User.Identity.IsAuthenticated)
            {
                var userName = this.HttpContext.User.Identity.Name;
                var currentUser = this.usersService.GetByUserName(userName);

                model.FirstName = currentUser.FirstName;
                model.LastName = currentUser.LastName;
                model.Email = currentUser.Email;
                model.PhoneNumber = currentUser.PhoneNumber;
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Contact(ContactFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                //TODO: fill form if user is logged + thank you message
                string subject = $"Запитване от {model.FirstName} {model.LastName}";
                string message = $"{model.FirstName} {model.LastName}\nemail: {model.Email}\nphone number: {model.PhoneNumber}\nmessage: {model.Message}";

                var mimeMessage = new MimeMessage()
                {
                    Subject = subject,
                    Body = new TextPart("plain")
                    {
                        Text = message
                    }
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
