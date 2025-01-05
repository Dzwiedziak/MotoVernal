using Microsoft.AspNetCore.Mvc;
using Entities = DB.Entities;

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
