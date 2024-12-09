using BusinessLogic.DTO.Ban;
using BusinessLogic.DTO.Event;
using BusinessLogic.Errors;
using BusinessLogic.Services;
using BusinessLogic.Services.Interfaces;
using DB.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MotoVendor.Controllers
{
    public class CalendarController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IBanService _banService;
        private readonly UserManager<User> _userManager;

        public CalendarController(IEventService eventService, IBanService banService, UserManager<User> userManager)
        {
            _eventService = eventService;
            _banService = banService;
            _userManager = userManager;
        }
        [Authorize]
        [HttpGet]
        public IActionResult EventsList()
        {
            var events = _eventService.GetEvents();
            return View(events);
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AddEvent()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            
            var result = _banService.GetActiveBan(currentUser.Id);
            if (result != null)
            {
                TempData["ErrorMessage"] = "You are blocked you cannot actually plan new event.";
                return RedirectToAction("Error", "Home");
            }
            var model = new AddEventDTO
            {
                Publisher = currentUser,
                EventType = DB.Enums.EventType.Physical,
                TimeFrom = DateTime.Today,
                TimeTo = DateTime.Today.AddDays(1),
                Title = string.Empty,
                Location = string.Empty,
                Description = string.Empty,
                Image = null
            };
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddEvent(AddEventDTO model)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var isBanned = _banService.GetActiveBan(currentUser.Id);
            if (isBanned != null)
            {
                TempData["ErrorMessage"] = "You are blocked you cannot actually plan new event.";
                return RedirectToAction("Error", "Home");
            }

            model.Publisher = currentUser;
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.Image?.Base64 == "defaultBase64Value" && model.Image?.Extension == "defaultExtension")
            {
                model.Image = null;
            }

            _eventService.Add(model);
            return RedirectToAction("EventsList");
        }
        public IActionResult EditEvent()
        {
            return View();
        }
        [Authorize]
        [HttpGet]
        public IActionResult DetailsEvent(int Id)
        {
            var result = _eventService.Get(Id);

            var currentUserId = _userManager.GetUserId(User);
            if(result.Value != null)
            {
                bool isOwner = currentUserId == result.Value.Publisher.Id;
                ViewBag.isOwner = isOwner;
                return View(result.Value);
            }
            else if (result.Error == EventErrorCode.EventNotFound)
            {
                TempData["ErrorMessage"] = "Event not exist";
                return RedirectToAction("Error", "Home");
            }
            return View(result.Value);
        }
    }
}
