using Microsoft.AspNetCore.Mvc;

namespace Http.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
