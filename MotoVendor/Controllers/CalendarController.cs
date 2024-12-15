using BusinessLogic.DTO.Ban;
using BusinessLogic.DTO.Event;
using BusinessLogic.DTO.EventInterest;
using BusinessLogic.DTO.User;
using BusinessLogic.DTO.UserObservation;
using BusinessLogic.Errors;
using BusinessLogic.Services;
using BusinessLogic.Services.Interfaces;
using DB.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MotoVendor.ViewModels;
using System.Drawing;
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
        public IActionResult EventsList(string sortBy, string search)
        {
            var events = _eventService.GetEvents();
            if (!string.IsNullOrEmpty(search))
            {
                events = events.Where(e => e.Title.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            switch (sortBy)
            {
                case "alphabetical_asc":
                    events = events.OrderBy(e => e.Title).ToList();
                    break;
                case "alphabetical_desc":
                    events = events.OrderByDescending(e => e.Title).ToList();
                    break;
                case "date_adc":
                    events = events.OrderBy(e => e.TimeFrom).ToList(); 
                    break;
                case "date_desc":
                    events = events.OrderByDescending(e => e.TimeFrom).ToList(); 
                    break;
                default:
                    events = events.OrderBy(e => e.TimeFrom).ToList();
                    break;
            }
            var eventsWithInterest = events.Select(e => new EventDTO
            {
                Id = e.Id,
                Publisher = e.Publisher,
                Title = e.Title,
                Description = e.Description,
                Location = e.Location,
                TimeFrom = e.TimeFrom,
                TimeTo = e.TimeTo,
                Image = e.Image,
                InterestedCount = _eventService.GetAllInterestByEvent(e.Id).Count()
            }).ToList();
            ViewBag.SortBy = sortBy;
            return View(eventsWithInterest);
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
            var currentUser = await _userManager.FindByIdAsync(model.Publisher.Id);
            model.Publisher = currentUser;
            if (model.Image?.Base64 == "defaultBase64Value" && model.Image?.Extension == "defaultExtension")
            {
                model.Image = null;
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            var isBanned = _banService.GetActiveBan(currentUser.Id);
            if (isBanned != null)
            {
                TempData["ErrorMessage"] = "You are blocked you cannot actually plan new event.";
                return RedirectToAction("Error", "Home");
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
            if (updateEventDTO.Image?.Base64 == "defaultBase64Value" && updateEventDTO.Image?.Extension == "defaultExtension")
            {
                updateEventDTO.Image = null;
            }
            if (!ModelState.IsValid)
            {
                return View(updateEventDTO);
            }
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

            _eventService.Update(updateEventDTO.Id, updateEventDTO);
            return RedirectToAction("DetailsEvent", new { id = updateEventDTO.Id });
        }

        [Authorize]
        [HttpGet]
        public IActionResult DetailsEvent(int Id)
        {
            var result = _eventService.Get(Id);
            var interestedUsers = _eventService.GetAllInterestByEvent(Id);

            ViewBag.EventID = Id;

            var currentUserId = _userManager.GetUserId(User);
            bool isCurrentUserInterested = interestedUsers.Any(u => u.User.Id == currentUserId);

            ViewBag.IsCurrentUserInterested = isCurrentUserInterested;
            if (result.Value != null)
            {
                bool isOwner = currentUserId == result.Value.Publisher.Id;
                ViewBag.isOwner = isOwner;
                var viewModel = new EventDetailsViewModel
                {
                    EventDetails = result.Value,
                    InterestedUsers = interestedUsers
                };
                return View(viewModel);
            }
            else if (result.Error == EventErrorCode.EventNotFound)
            {
                TempData["ErrorMessage"] = "Event not exist";
                return RedirectToAction("Error", "Home");
            }
            return View();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> InterestEvent(int id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var result = _eventService.Get(id);
            if (result.Value == null)
            {
                TempData["ErrorMessage"] = "Event not exist";
                return RedirectToAction("Error", "Home");
            }
            if (currentUser.Id == result.Value.Publisher.Id)
            {
                TempData["ErrorMessage"] = "Uou can't get interested in your own event";
                return RedirectToAction("Error", "Home");
            }
            var currentEvent = _eventService.GetEvent(id);
            var model = new AddEventInterestDTO
            {
                User = currentUser,
                Event = currentEvent
            };
            var interestResult = _eventService.InterestUser(model);
            if (interestResult == null)
            {
                return RedirectToAction("DetailsEvent", new { id = model.Event.Id });
            }
            else if (interestResult == EventInterestErrorCode.AlreadyInterested)
            {
                TempData["ErrorMessage"] = "You already interested in this event.";
                return RedirectToAction("Error", "Home");
            }
            return RedirectToAction("DetailsEvent", new { id = model.Event.Id });
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UninterestEvent(int id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var result = _eventService.Get(id);
            if (result.Value == null)
            {
                TempData["ErrorMessage"] = "Event not exist";
                return RedirectToAction("Error", "Home");
            }
            var currentEvent = _eventService.GetEvent(id);
            var model = new DeleteEventInterestDTO
            {
                User = currentUser,
                Event = currentEvent
            };
            var interestResult = _eventService.StopInterestUser(model);
            if (interestResult == null)
            {
                return RedirectToAction("DetailsEvent", new { id = model.Event.Id });
            }
            else if (interestResult == EventInterestErrorCode.AlreadyNotInterested)
            {
                TempData["ErrorMessage"] = "You already not interested in this event.";
                return RedirectToAction("Error", "Home");
            }
            return RedirectToAction("DetailsEvent", new { id = model.Event.Id });
        }
    }
}
