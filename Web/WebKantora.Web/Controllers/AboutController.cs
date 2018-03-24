using AspNetSeo.CoreMvc;
using Microsoft.AspNetCore.Mvc;

namespace WebKantora.Web.Controllers
{
    [SeoBaseTitle("��� �������")]
    public class AboutController : Controller
    {
        [SeoTitle("")]
        [SeoMetaDescription("")]
        public IActionResult Index()
        {
            this.GetSeoHelper().Title = "";
            return View();
        }
    }
}