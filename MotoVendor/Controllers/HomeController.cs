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

        [HttpGet]
        public IActionResult Test()
        {
            Entities.File file = new Entities.File()
            {
                Base64 = "",
                Extension = ""
            };
            return View(file);
        }

        [HttpPost]
        public IActionResult Test(Entities.File file)
        {
            return View(file);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
