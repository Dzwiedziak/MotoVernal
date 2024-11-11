using Microsoft.AspNetCore.Mvc;

namespace MotoVendor.Controllers
{
    public class NewsController : Controller
    {
        public IActionResult PostsList()
        {
            return View();
        }
        public IActionResult AddPost()
        {
            return View();
        }
        public IActionResult EditPost()
        {
            return View();
        }
    }
}
