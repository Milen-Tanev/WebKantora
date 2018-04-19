using Microsoft.AspNetCore.Mvc;

using WebKantora.Services.Web.Contracts;
using WebKantora.Services.Data.Contracts;
using AspNetSeo.CoreMvc;

namespace WebKantora.Web.Controllers
{
    [SeoBaseTitle("Уеб кантора - правни услуги онлайн")]
    public class HomeController : BaseController
    {

        public HomeController()
        {
        }

        public IActionResult Index()
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
