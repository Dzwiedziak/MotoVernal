using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MotoVendor.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult IntroductionPage()
        {
            return View();
        }
    }
}
