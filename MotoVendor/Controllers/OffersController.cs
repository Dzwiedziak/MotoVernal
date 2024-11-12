using Microsoft.AspNetCore.Mvc;

namespace MotoVendor.Controllers
{
    public class OffersController : Controller
    {
        public IActionResult PromotedOffers()
        {
            return View();
        }
        public IActionResult ExchangeRatesInfo()
        {
            return View();
        }
    }
}
