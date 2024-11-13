using Microsoft.AspNetCore.Mvc;

namespace MotoVendor.Controllers
{
    public class AppController : Controller
    {
        
        public IActionResult BanAccount()
        {
            return View();
        }
        public IActionResult EditProfile()
        {
            return View();
        }
    }
}
