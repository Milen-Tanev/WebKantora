using AutoMapper;
using MimeKit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using WebKantora.Data.Models;
using WebKantora.Services.Web.Contracts;
using WebKantora.Services.Data.Contracts;
using WebKantora.Web.Models.ContactViewModels;
using AspNetSeo.CoreMvc;

namespace WebKantora.Web.Controllers
{
    [SeoBaseTitle("Уеб кантора - правни услуги онлайн")]
    public class MessageController : BaseController
    {
        private IMessagesService messagesService;
        private IUsersService usersService;
        private IEmailSenderService emailSenderService;
        private ICustomErrorService customErrors;
        private IMapper mapper;

        public MessageController(IMessagesService messagesService,
            IUsersService usersService,
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

        [HttpPost]
        public async Task<IActionResult> Send(ContactFormViewModel model)
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
                    try
                    {
                        await this.messagesService.Add(message);
                    }
                    catch (System.Exception ex)
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
                    }

                    return this.RedirectToAction("Index", "Home");
                }
                else
                {
                    return this.BadRequest();
                }
            }
            
            return this.View(model);
        }
    }
}