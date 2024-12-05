using BusinessLogic.DTO.Offer;
using BusinessLogic.DTO.VehicleOffer;
using BusinessLogic.Errors;
using BusinessLogic.Services;
using BusinessLogic.Services.Interfaces;
using DB.Enums;
using Microsoft.AspNetCore.Mvc;

namespace MotoVendor.Controllers
{
    public class StoreController : Controller
    {
        
        IVehicleOfferService _vehicleOfferService;
        IUserService _userService;
        public StoreController(IVehicleOfferService vehicleOfferService, IUserService userService)
        {
            _vehicleOfferService = vehicleOfferService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult AddOffer()
        {
            var model = new AddVehicleOfferDTO("Audi", "A6", "C7", "3", TransmissionType.Manual, VehicleDriveType.FrontWheel, BodyType.Sedan, "blue", VehicleCondition.Used, 5, 1999, 2555, OwnerType.First, "44ueu", "elo elo320", "Warszawa", new DB.Entities.User(), "mm@gmail.com", "+48 433 434 433", 4344343, new List<DB.Entities.File>());
            return View(model);
        }
        [HttpPost]
        public IActionResult AddOffer(AddVehicleOfferDTO vehicleOffer)
        {
            vehicleOffer.User = _userService.GetCurrentUser().Result;
            _vehicleOfferService.Add(vehicleOffer);
            return RedirectToAction("DetailsOffer");
        }

        public IActionResult VehiclesList()
        {
            return View();
        }
        public IActionResult SearchOffers()
        {
            return View();
        }
        public IActionResult DetailsOffer(int id)
        {
            var model = _vehicleOfferService.Get(id);
            if(model.IsSuccess)
                return View(model.Value);
            return View("Error");
        }

    }
}
