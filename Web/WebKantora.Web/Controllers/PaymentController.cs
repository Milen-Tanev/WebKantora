using Microsoft.AspNetCore.Mvc;
using AspNetSeo.CoreMvc;

namespace WebKantora.Web.Controllers
{
    [SeoBaseTitle("��� ������� - ������ ������ ������")]
    public class PaymentController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}