using Microsoft.AspNetCore.Mvc;
using AspNetSeo.CoreMvc;

namespace WebKantora.Web.Controllers
{
    [SeoBaseTitle("Уеб кантора - правни услуги онлайн")]
    public class PaymentController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}