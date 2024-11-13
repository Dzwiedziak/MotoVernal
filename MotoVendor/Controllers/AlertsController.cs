using Microsoft.AspNetCore.Mvc;

namespace MotoVendor.Controllers
{
    public class AlertsController : Controller
    {
        public IActionResult NotificationsView()
        {
            return View();
        }
    }
}
