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
        public StoreController(IVehicleOfferService vehicleOfferService)
        {
            _vehicleOfferService = vehicleOfferService;
        }

        [HttpGet]
        public IActionResult AddOffer()
        {
            //var model = new AddVehicleOfferDTO("", "", "", "", TransmissionType.Manual, VehicleDriveType.FrontWheel, BodyType.Sedan, "", VehicleCondition.Used, );
            return View();
        }
        [HttpPost]
        public IActionResult AddOffer(AddVehicleOfferDTO vehicleOffer)
        {
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
        public IActionResult DetailsOffer()
        {
            return View();
        }

    }
}
