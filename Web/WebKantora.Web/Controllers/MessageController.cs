using AutoMapper;
using MimeKit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using WebKantora.Data.Models;
using WebKantora.Services.Web.Contracts;
using WebKantora.Services.Data.Contracts;
using WebKantora.Web.Models.ContactViewModels;
using AspNetSeo.CoreMvc;
using System;
using WebKantora.Web.Models.CustomErrorViewModels;

namespace WebKantora.Web.Controllers
{
    [SeoBaseTitle("Уеб кантора - правни услуги онлайн")]
    public class MessageController : BaseController
    {
        private IMessageService messagesService;
        private IUserService usersService;
        private IEmailSenderService emailSenderService;
        private ICustomErrorService customErrors;
        private IMapper mapper;

        public MessageController(
            IMessageService messagesService,
            IUserService usersService,
            IEmailSenderService emailSenderService,
            ICustomErrorService customErrors,
            IMapper mapper)
        {
            this.messagesService = messagesService;
            this.usersService = usersService;
            this.emailSenderService = emailSenderService;
            this.customErrors = customErrors;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var model = new ContactFormViewModel();

                if (User.Identity.IsAuthenticated)
                {
                    var userName = this.HttpContext.User.Identity.Name;
                    var currentUser = await this.usersService.GetByUserName(userName);

                    model.FirstName = currentUser.FirstName;
                    model.LastName = currentUser.LastName;
                    model.Email = currentUser.Email;
                    model.PhoneNumber = currentUser.PhoneNumber;
                }
                return View(model);
            }
            catch (Exception ex)
            {
                var error = new CustomError()
                {
                    InnerException = "",
                    Message = ex.Message,
                    Source = ex.Source,
                    StackTrace = ex.StackTrace,
                    CustomMessage = ""
                };

                await this.customErrors.Add(error);
                return this.View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Send(ContactFormViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string subject = $"Запитване от {model.FirstName} {model.LastName}";
                    string messageText = $"{model.FirstName} {model.LastName}\nemail: {model.Email}\nphone number: {model.PhoneNumber}\nmessage: {model.Content}";

                    var mimeMessage = new MimeMessage()
                    {
                        Subject = subject,
                        Body = new TextPart("plain")
                        {
                            Text = messageText
                        }
                    };

                    var result = this.emailSenderService.SendEmailForUserRequest(mimeMessage);

                    if (result)
                    {
                        TempData.Add("SuccessMessage", "Благодарим Ви за запитването!");

                        var message = this.mapper.Map<Message>(model);

                        if (User.Identity.IsAuthenticated)
                        {
                            var user = await this.usersService.GetByUserName(User.Identity.Name);
                            message.Author = user;
                        }
                        await this.messagesService.Add(message);

                        return this.RedirectToAction("Index", "Home");
                    }
                }

                return this.View(model);
            }
            catch (Exception ex)
            {
                var error = new CustomError()
                {
                    InnerException = "стр",
                    Message = ex.Message,
                    Source = ex.Source,
                    StackTrace = ex.StackTrace,
                    CustomMessage = ""
                };

                await this.customErrors.Add(error);

                var errorModel = new CustomErrorViewModel()
                {
                    ErrorContent = "Възникна грешка при изпращането на Вашето съобщение. Моля, опитайте по-късно."
                };

                return View(errorModel);
            }
        }
    }
}