using WebKantora.Web.Models.CustomErrorViewModels;

using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Error(CustomErrorViewModel errorModel)
        {
            return View(errorModel);
        }
    }
}
