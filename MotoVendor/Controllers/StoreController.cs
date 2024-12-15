using BusinessLogic.DTO.VehicleOffer;
using BusinessLogic.Services.Interfaces;
using DB.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotoVendor.Authorizations.Requirements;

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
            if (authorizationResult.Succeeded)
            {
                var error = _vehicleOfferService.Update(vehicleOffer);
                if (error is null)
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
            var user = _userService.GetCurrentUser().Result;
            var query = HttpContext.Request.Query;

            var filters = new Dictionary<string, HashSet<string>>();
            string? sortBy = null;
            string? isOwnerQuery = null;
            string? isObservingQuery = null;

            foreach (var key in query.Keys)
            {
                var values = query[key];
                if (key.Equals("sort_by", StringComparison.OrdinalIgnoreCase))
                {
                    sortBy = values.FirstOrDefault();
                }
                else if (key.Equals("isOwner", StringComparison.OrdinalIgnoreCase))
                {
                    isOwnerQuery = values.FirstOrDefault();
                }
                else if (key.Equals("isObserving", StringComparison.OrdinalIgnoreCase))
                {
                    isObservingQuery = values.FirstOrDefault();
                }
                else
                {
                    foreach (var value in values)
                    {
                        if (!string.IsNullOrWhiteSpace(value))
                        {
                            if (!filters.ContainsKey(key))
                            {
                                filters[key] = new HashSet<string>();
                            }
                            filters[key].Add(value);
                        }
                    }
                }
            }

            var vehicleOffers = _vehicleOfferService.GetAll();

            var filteredOffers = ListFilter.FilterList(vehicleOffers, filters);

            ViewBag.IsOwner = isOwnerQuery;
            if (bool.TryParse(isOwnerQuery, out var isOwner))
            {
                if (isOwner)
                {
                    if (user == null)
                    {
                        return View(new List<GetVehicleOfferDTO>());
                    }

                    filteredOffers = filteredOffers.Where(o => o.User.Id == user.Id).ToList();
                }
                else
                {
                    if (user != null)
                    {
                        filteredOffers = filteredOffers.Where(o => o.User.Id != user.Id).ToList();
                    }
                }
            }

            ViewBag.IsObserving = isObservingQuery;
            if (bool.TryParse(isObservingQuery, out var isObserving))
            {
                if (isObserving)
                {
                    if (user == null)
                    {
                        return View(new List<GetVehicleOfferDTO>());
                    }

                    filteredOffers = filteredOffers.Where(o => _vehicleOfferService.IsObservedBy(user.Id, o.Id)).ToList();
                }
                else
                {
                    if (user != null)
                    {
                        filteredOffers = filteredOffers.Where(o => _vehicleOfferService.IsObservedBy(user.Id, o.Id)).ToList();
                    }
                }
            }

            var sortedOffers = ListFilter.SortList(filteredOffers, sortBy);

            var paginatedOffers = ListFilter.GetPaginatedList(sortedOffers, pageIndex, pageSize);

            ViewBag.FilterValues = filters;
            ViewBag.SortBy = sortBy;
            ViewBag.PageIndex = pageIndex;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItemsCount = filteredOffers.Count;

            return View(paginatedOffers);
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
            if (model.IsSuccess)
                return View(model.Value);
            return View("Error");
        }
        public IActionResult ToggleOfferObservation(int offerId)
        {
            return View();
        }

    }
}
