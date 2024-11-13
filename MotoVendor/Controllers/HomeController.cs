using Microsoft.AspNetCore.Mvc;

namespace MotoVendor.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult IntroductionPage()
        {
            return View();
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
