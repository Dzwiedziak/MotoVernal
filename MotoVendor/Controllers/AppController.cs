using Microsoft.AspNetCore.Mvc;

namespace MotoVendor.Controllers
{
    public class AppController : Controller
    {
        public IActionResult ProfileView()
        {
            return View();
        }
        public IActionResult BanAccount()
        {
            return View();
        }
        public IActionResult EditProfile()
        {
            return View();
        }
        public IActionResult ProfilesList()
        {
            return View();
        }
    }
}
