using Microsoft.AspNetCore.Mvc;
using Entities = DB.Entities;

namespace MotoVernal.Controllers
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
        public IActionResult FAQ()
        {
            return View();
        }
        public IActionResult ExchangeRatesInfo()
        {
            return View();
        }
        public IActionResult APIDocumentation()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult FutureFeatures()
        {
            return View();
        }
    }
}
