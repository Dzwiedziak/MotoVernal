using BusinessLogic.DTO.VehicleOffer;
using BusinessLogic.Errors;
using BusinessLogic.Services;
using BusinessLogic.Services.Interfaces;
using BusinessLogic.Services.Response;
using DB.Entities;
using DB.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MotoVendor.Authorizations.Requirements;

namespace MotoVendor.Controllers
{
    public class StoreController : Controller
    {

        readonly IVehicleOfferService _vehicleOfferService;
        readonly IUserService _userService;
        readonly UserManager<User> _userManager;
        readonly IAuthorizationService _authorizationService;
        readonly IBanService _banService;
        public StoreController(IVehicleOfferService vehicleOfferService, IUserService userService, IAuthorizationService authorizationService, IBanService banService, UserManager<User> userManager)
        {
            _vehicleOfferService = vehicleOfferService;
            _userService = userService;
            _userManager = userManager;
            _authorizationService = authorizationService;
            _banService = banService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AddOffer()
        {
            var model = new AddVehicleOfferDTO("Audi", "A6", "C7", "3", TransmissionType.Manual, VehicleDriveType.FrontWheel, BodyType.Sedan, "blue", VehicleCondition.Used, 5, 1999, 2555, OwnerType.First, "44ueu", "elo elo320", "Warszawa", new DB.Entities.User(), "mm@gmail.com", "+48 433 434 433", 4344343, new List<DB.Entities.File>());
            var user = await _userManager.GetUserAsync(User);
            model.User.Id = user.Id;
            return View(model);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddOffer(AddVehicleOfferDTO vehicleOffer)
        {
            if(!ModelState.IsValid)
            {
                return View(vehicleOffer);
            }
            var currentUser = await _userManager.GetUserAsync(User);
            var isBanned = _banService.GetActiveBan(currentUser.Id);
            if (isBanned != null)
            {
                TempData["ErrorMessage"] = "You are blocked you cannot actually plan new event.";
                return RedirectToAction("Error", "Home");
            }
            vehicleOffer.User = currentUser;
            var result = _vehicleOfferService.Add(vehicleOffer);
            if(result.IsSuccess)
                return RedirectToAction("VehiclesList");
            switch(result.Error)
            {
                case VehicleOfferErrorCode.VehicleWithVinExists:
                    ModelState.AddModelError("VIN", "Vin exists in database. Contact with administrator");
                    return View(vehicleOffer);
                default:
                    TempData["ErrorMessage"] = "Unknown error happened";
                    return RedirectToAction("Error", "Home");
            }
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditOffer(int id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var isBanned = _banService.GetActiveBan(currentUser.Id);
            if (isBanned != null)
            {
                TempData["ErrorMessage"] = "You are blocked you cannot actually plan new event.";
                return RedirectToAction("Error", "Home");
            }
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
            if (!ModelState.IsValid)
            {
                return View(vehicleOffer);
            }
            var currentUser = await _userManager.GetUserAsync(User);
            var isBanned = _banService.GetActiveBan(currentUser.Id);
            if (isBanned != null)
            {
                TempData["ErrorMessage"] = "You are blocked you cannot actually plan new event.";
                return RedirectToAction("Error", "Home");
            }
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, null, new IsVehicleOfferOwnerRequirement(vehicleOffer.Id));
            if (authorizationResult.Succeeded)
            {
                var error = _vehicleOfferService.Update(vehicleOffer);
                if (error is null)
                {
                    return RedirectToAction("VehiclesList");
                }
                switch (error)
                {
                    case VehicleOfferErrorCode.VehicleWithVinExists:
                        ModelState.AddModelError("VIN", "Vin exists in database. Contact with administrator");
                        return View(vehicleOffer);
                    default:
                        TempData["ErrorMessage"] = "Unknown error happened";
                        return RedirectToAction("Error", "Home");
                }
            }
            TempData["ErrorMessage"] = "You have no permission to edit this offer";
            return RedirectToAction("Error", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> VehiclesList(int pageIndex = 0, int pageSize = 30)
        {
            var currentUser = await _userManager.GetUserAsync(User);
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
                    if (currentUser == null)
                    {
                        return View(new List<GetVehicleOfferDTO>());
                    }

                    filteredOffers = filteredOffers.Where(o => o.User.Id == currentUser.Id).ToList();
                }
                else
                {
                    if (currentUser != null)
                    {
                        filteredOffers = filteredOffers.Where(o => o.User.Id != currentUser.Id).ToList();
                    }
                }
            }

            ViewBag.IsObserving = isObservingQuery;
            if (bool.TryParse(isObservingQuery, out var isObserving))
            {
                if (isObserving)
                {
                    if (currentUser == null)
                    {
                        return View(new List<GetVehicleOfferDTO>());
                    }

                    filteredOffers = filteredOffers.Where(o => _vehicleOfferService.IsObservedBy(currentUser.Id, o.Id)).ToList();
                }
                else
                {
                    if (currentUser != null)
                    {
                        filteredOffers = filteredOffers.Where(o => _vehicleOfferService.IsObservedBy(currentUser.Id, o.Id)).ToList();
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
        public async Task<IActionResult> DetailsOffer(int id)
        {
            var deletePermsission = await _authorizationService.AuthorizeAsync(User, null, new CanDeleteVehicleOfferRequirement(id));
            var editPermission = await _authorizationService.AuthorizeAsync(User, null, new IsVehicleOfferOwnerRequirement(id));
            ViewBag.CanDelete = deletePermsission.Succeeded;
            ViewBag.CanEdit = editPermission.Succeeded;
            var model = _vehicleOfferService.Get(id);
            if (model.IsSuccess)
                return View(model.Value);
            return View("Error");
        }

        [Authorize]
        [Route("{id}/delete")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var isBanned = _banService.GetActiveBan(currentUser.Id);
            if (isBanned != null)
            {
                TempData["ErrorMessage"] = "You are blocked you cannot actually plan new event.";
                return RedirectToAction("Error", "Home");
            }
            var permission = await _authorizationService.AuthorizeAsync(User, null, new CanDeleteVehicleOfferRequirement(id));
            if (!permission.Succeeded)
            {
                TempData["ErrorMessage"] = "You have not permissions to this action";
                return RedirectToAction("Error", "Home");
            }
            var error = _vehicleOfferService.Delete(id);
            if (error == null)
            {
                return RedirectToAction("VehiclesList");
            }
            TempData["ErrorMessage"] = "Offer has not been found";
            return RedirectToAction("Error", "Home");
        }
    }
}
