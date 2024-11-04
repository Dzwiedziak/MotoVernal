using Microsoft.AspNetCore.Mvc;

namespace Http.Controllers
{
    public class IntroductionPageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
