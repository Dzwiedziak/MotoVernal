using Microsoft.AspNetCore.Mvc;

namespace MotoVendor.Controllers
{
    public class ProblemController : Controller
    {
        public IActionResult Report()
        {
            return View();
        }
    }
}
