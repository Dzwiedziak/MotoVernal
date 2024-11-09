using Microsoft.AspNetCore.Mvc;

namespace MotoVendor.Controllers
{
    public class ForumController : Controller
    {
        public IActionResult SectionsList()
        {
            return View();
        }
    }
}
