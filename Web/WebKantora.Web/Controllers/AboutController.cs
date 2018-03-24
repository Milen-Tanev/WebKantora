using AspNetSeo.CoreMvc;
using Microsoft.AspNetCore.Mvc;

namespace WebKantora.Web.Controllers
{
    [SeoBaseTitle("Уеб кантора - правни услуги онлайн")]
    public class AboutController : BaseController
    {
        public IActionResult Index()
        {
            this.GetSeoHelper().Title = "За нас";
            return View();
        }
    }
}