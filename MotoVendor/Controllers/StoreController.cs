using BusinessLogic.DTO.Offer;
using BusinessLogic.DTO.VehicleOffer;
using BusinessLogic.Errors;
using BusinessLogic.Repositories;
using BusinessLogic.Services;
using BusinessLogic.Services.Interfaces;
using DB.Entities;
using DB.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;
using MotoVendor.Authorizations.Requirements;
using System.Collections.Generic;

namespace MotoVendor.Controllers
{
    public class StoreController : Controller
    {

        IVehicleOfferService _vehicleOfferService;
        IUserService _userService;
        IAuthorizationService _authorizationService;
        public StoreController(IVehicleOfferService vehicleOfferService, IUserService userService, IAuthorizationService authorizationService)
        {
            _vehicleOfferService = vehicleOfferService;
            _userService = userService;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult AddOffer()
        {
            var model = new AddVehicleOfferDTO("Audi", "A6", "C7", "3", TransmissionType.Manual, VehicleDriveType.FrontWheel, BodyType.Sedan, "blue", VehicleCondition.Used, 5, 1999, 2555, OwnerType.First, "44ueu", "elo elo320", "Warszawa", new DB.Entities.User(), "mm@gmail.com", "+48 433 434 433", 4344343, new List<DB.Entities.File>());
            return View(model);
        }
        [HttpPost]
        [Authorize]
        public IActionResult AddOffer(AddVehicleOfferDTO vehicleOffer)
        {
            vehicleOffer.User = _userService.GetCurrentUser().Result;
            _vehicleOfferService.Add(vehicleOffer);
            return RedirectToAction("VehiclesList");
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditOffer(int id)
        {
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, null, new IsVehicleOfferOwnerRequirement(id));
            if (authorizationResult.Succeeded)
            {
                var model = _vehicleOfferService.GetUpdateDTO(id);
                if (model != null)
                    return View(model);
            }
            return View("Error");
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditOffer(UpdateVehicleOfferDTO vehicleOffer)
        {
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, null, new IsVehicleOfferOwnerRequirement(vehicleOffer.Id));
            if(authorizationResult.Succeeded)
            {
                var error = _vehicleOfferService.Update(vehicleOffer);
                if(error is null)
                {
                    return RedirectToAction("VehiclesList");
                }
                return View("Error");
            }
            return View("Error");
        }

        [HttpGet]
        public IActionResult VehiclesList(int pageIndex = 0, int pageSize = 30)
        {
            var query = HttpContext.Request.Query;
            var filters = new Dictionary<string, HashSet<string>>();
            string sort_by = null;
            foreach(var key in query.Keys)
            {
                var value = query[key];
                if(key == "sort_by")
                    sort_by = value;
                else
                {
                    foreach (var keyvalue in value)
                    {
                        if (!filters.ContainsKey(key))
                        {
                            filters[key] = new HashSet<string>();
                        }
                        if (!keyvalue.IsNullOrEmpty())
                            filters[key].Add(keyvalue);
                    }
                }
            }
            List<GetVehicleOfferDTO> getVehicleOfferDTOs = _vehicleOfferService.GetAll();
            List<GetVehicleOfferDTO> filteredGetVehicleOfferDTOs = ListFilter.FilterList(getVehicleOfferDTOs, filters);
            List<GetVehicleOfferDTO> sortedList = ListFilter.SortList(filteredGetVehicleOfferDTOs, sort_by);
            var paginatedList = ListFilter.GetPaginatedList(sortedList, pageIndex, pageSize);
            ViewBag.FilterValues = filters;
            ViewBag.SortBy = sort_by;
            ViewBag.PageIndex = pageIndex;
            ViewBag.PageSize = pageSize;
            @ViewBag.TotalItemsCount = filteredGetVehicleOfferDTOs.Count;
            return View(paginatedList);
        }
        [HttpGet]
        public IActionResult SearchOffers()
        {
            return View();
        }
        [HttpGet]
        public IActionResult DetailsOffer(int id)
        {
            var model = _vehicleOfferService.Get(id);
            if(model.IsSuccess)
                return View(model.Value);
            return View("Error");
        }
        public IActionResult ToggleOfferObservation(int offerId)
        {
            
        }

    }
}
