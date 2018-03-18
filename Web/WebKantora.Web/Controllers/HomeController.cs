using Microsoft.AspNetCore.Mvc;

using WebKantora.Services.Web.Contracts;
using WebKantora.Web.Models.ContactViewModels;
using MimeKit;
using WebKantora.Services.Data.Contracts;
using System.Threading.Tasks;

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

        [Route("/Home/Error")]
        public IActionResult Error()
        {
            return View();
        }
    }
}
