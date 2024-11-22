using Microsoft.AspNetCore.Mvc;

namespace MotoVendor.Controllers
{
    public class AppController : Controller
    {
        
        public IActionResult BanAccount()
        {
            return View();
        }
    }
}
