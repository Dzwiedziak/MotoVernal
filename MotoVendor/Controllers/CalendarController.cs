using Microsoft.AspNetCore.Mvc;

namespace MotoVendor.Controllers
{
    public class CalendarController : Controller
    {
        public IActionResult EventsList()
        {
            return View();
        }
        public IActionResult AddEvent()
        {
            return View();
        }
        public IActionResult EditEvent()
        {
            return View();
        }
        public IActionResult DetailsEvent()
        {
            return View();
        }
    }
}
