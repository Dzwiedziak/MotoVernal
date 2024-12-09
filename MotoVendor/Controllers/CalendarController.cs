using BusinessLogic.DTO.Ban;
using BusinessLogic.DTO.Event;
using BusinessLogic.DTO.User;
using BusinessLogic.Errors;
using BusinessLogic.Services;
using BusinessLogic.Services.Interfaces;
using DB.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> EditEvent(int id)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var resultBan = _banService.GetActiveBan(currentUser.Id);
            if (resultBan != null)
            {
                TempData["ErrorMessage"] = "You are blocked you cannot edit your event.";
                return RedirectToAction("Error", "Home");
            }
            var result = _eventService.Get(id);

            var currentUserId = _userManager.GetUserId(User);
            if (result.Value == null)
            {
                TempData["ErrorMessage"] = "Event not exist";
                return RedirectToAction("Error", "Home");
            }
            else if (currentUserId != result.Value.Publisher.Id)
            {
                TempData["ErrorMessage"] = "You are not a owner of this event";
                return RedirectToAction("Error", "Home");
            }

            var model = new UpdateEventDTO
            {
                Id = id,
                EventType = result.Value.EventType,
                TimeFrom = result.Value.TimeFrom,
                TimeTo = result.Value.TimeTo,
                Title = result.Value.Title,
                Location = result.Value.Location,
                Description = result.Value.Description,
                Image = result.Value.Image
            };
            return View(model);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditEvent(UpdateEventDTO updateEventDTO)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var resultBan = _banService.GetActiveBan(currentUser.Id);
            if (resultBan != null)
            {
                TempData["ErrorMessage"] = "You are blocked you cannot edit your event.";
                return RedirectToAction("Error", "Home");
            }
            var result = _eventService.Get(updateEventDTO.Id);
            if (result.Value == null)
            {
                TempData["ErrorMessage"] = "Event not exist";
                return RedirectToAction("Error", "Home");
            }
            else if (currentUser.Id != result.Value.Publisher.Id)
            {
                TempData["ErrorMessage"] = "You are not a owner of this event";
                return RedirectToAction("Error", "Home");
            }

            if (!ModelState.IsValid)
            {
                return View(updateEventDTO);
            }
            if (updateEventDTO.Image?.Base64 == "defaultBase64Value" && updateEventDTO.Image?.Extension == "defaultExtension")
            {
                updateEventDTO.Image = null;
            }

            _eventService.Update(updateEventDTO.Id, updateEventDTO);
            return RedirectToAction("DetailsEvent", new { id = updateEventDTO.Id });
        }

        [Authorize]
        [HttpGet]
        public IActionResult DetailsEvent(int Id)
        {
            var result = _eventService.Get(Id);

            ViewBag.EventID = Id;

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
