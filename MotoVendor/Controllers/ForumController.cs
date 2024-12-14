using BusinessLogic.DTO.Event;
using BusinessLogic.DTO.Section;
using BusinessLogic.DTO.Topic;
using BusinessLogic.DTO.TopicResponse;
using BusinessLogic.Errors;
using BusinessLogic.Services;
using BusinessLogic.Services.Interfaces;
using BusinessLogic.Services.Response;
using DB.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MotoVendor.ViewModels;
using static System.Collections.Specialized.BitVector32;

namespace MotoVendor.Controllers
{
    public class ForumController : Controller
    {
        private readonly ISectionService _sectionService;
        private readonly ITopicService _topicService;
        private readonly ITopicResponseService _topicResponseService;
        private readonly IBanService _banService;
        private readonly UserManager<User> _userManager;

        public ForumController(ISectionService sectionService, ITopicService topicService, ITopicResponseService topicResponseService, IBanService banService, UserManager<User> userManager)
        {
            _sectionService = sectionService;
            _topicService = topicService;
            _topicResponseService = topicResponseService;
            _banService = banService;
            _userManager = userManager;
        }
        public IActionResult SectionsAndTopicsList(int? sectionId)
        {
            
            var rootSectionResult = _sectionService.GetRootSection();
            if (!rootSectionResult.IsSuccess)
            {
                TempData["ErrorMessage"] = "Root section not found";
                return RedirectToAction("Error", "Home");
            }
            
            var activeSectionId = sectionId ?? rootSectionResult.Value.Id;

            ViewBag.IsRootSection = activeSectionId == rootSectionResult.Value.Id;

            var sectionInfo = _sectionService.Get(activeSectionId);
            var childSections = _sectionService.GetChildrenSections(activeSectionId);
            var parentSections = _sectionService.GetParentSections(activeSectionId);
            var topics = _topicService.GetAllInSections(activeSectionId);

            var vm = new SectionsAndTopicsListViewModel
            {
                SectionInfo = sectionInfo.Value,
                ChildSections = childSections.Value,
                ParentSections = parentSections.Value,
                TopicsList = topics
            };

            return View(vm);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult AddSection(int sectionId)
        {
            var parentSection = _sectionService.GetOne(sectionId);
            if (parentSection == null)
            {
                TempData["ErrorMessage"] = "Parent section not found";
                return RedirectToAction("Error", "Home");
            }
            var model = new AddSectionDTO
            {
                Title = string.Empty,
                Parent = parentSection,
                Image = null
            };
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddSection(AddSectionDTO model)
        {
            var parentSection = _sectionService.GetOne(model.Parent.Id);
            model.Parent = parentSection;
            if (model.Image?.Base64 == "defaultBase64Value" && model.Image?.Extension == "defaultExtension")
            {
                model.Image = null;
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _sectionService.Add(model);
            return RedirectToAction("SectionsAndTopicsList", new { sectionId = model.Parent.Id });
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult EditSection(int Id)
        {
            var sectionToEdit = _sectionService.GetOne(Id);
            if(sectionToEdit == null)
            {
                TempData["ErrorMessage"] = "Section of this id not found";
                return RedirectToAction("Error", "Home");
            }
            var model = new UpdateSectionDTO
            {
                Id = sectionToEdit.Id,
                Title = sectionToEdit.Title,
                Parent = sectionToEdit.Parent,
                Image = sectionToEdit.Image
            };
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult EditSection(UpdateSectionDTO model)
        {
            if (model.Image?.Base64 == "defaultBase64Value" && model.Image?.Extension == "defaultExtension")
            {
                model.Image = null;
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = _sectionService.Update(model);
            if(result == null)
            {
                return RedirectToAction("SectionsAndTopicsList", new { sectionId = model.Parent.Id });
            }
            else
            {
                TempData["ErrorMessage"] = "Section of this id not found";
                return RedirectToAction("Error", "Home");
            }
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AddTopic(int sectionId)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var result = _banService.GetActiveBan(currentUser.Id);
            if (result != null)
            {
                TempData["ErrorMessage"] = "You are blocked you cannot actually add new topic.";
                return RedirectToAction("Error", "Home");
            }
            var rootSectionResult = _sectionService.GetRootSection();
            if (!rootSectionResult.IsSuccess)
            {
                TempData["ErrorMessage"] = "Root section not found";
                return RedirectToAction("Error", "Home");
            }

            if(sectionId == rootSectionResult.Value.Id)
            {
                TempData["ErrorMessage"] = "You can't add topic in Main Section";
                return RedirectToAction("Error", "Home");
            }
            var currentSection = _sectionService.GetOne(sectionId);
            if (currentSection == null)
            {
                TempData["ErrorMessage"] = "Section not found";
                return RedirectToAction("Error", "Home");
            }
            var model = new AddTopicDTO
            {
               Title = string.Empty,
               Description = string.Empty,
               Publisher = currentUser,
               Section = currentSection,
               Image = null
            };
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddTopic(AddTopicDTO model)
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
            _topicService.Add(model);
            return RedirectToAction("SectionsAndTopicsList", new { sectionId = model.Section.Id });
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> EditTopic(int Id)
        {
            var topicToEdit = _topicService.GetOne(Id);
            if (topicToEdit == null)
            {
                TempData["ErrorMessage"] = "Topic of this id not found";
                return RedirectToAction("Error", "Home");
            }
            var currentUser = await _userManager.GetUserAsync(User);
            var publisherUser = await _userManager.FindByIdAsync(topicToEdit.Publisher.Id);
            if (currentUser != publisherUser) 
            {
                TempData["ErrorMessage"] = "It is not your topic";
                return RedirectToAction("Error", "Home");
            }
            var model = new UpdateTopicDTO
            {
                Id = topicToEdit.Id,
                Title = topicToEdit.Title,
                Description = topicToEdit.Description,
                Image = topicToEdit.Image
            };
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditTopic(UpdateTopicDTO model)
        {
            if (model.Image?.Base64 == "defaultBase64Value" && model.Image?.Extension == "defaultExtension")
            {
                model.Image = null;
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = _topicService.Update(model);
            if (result == null)
            {
                return RedirectToAction("DetailsTopic", new { Id = model.Id });
            }
            else
            {
                TempData["ErrorMessage"] = "Topic of this id not found";
                return RedirectToAction("Error", "Home");
            }
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> DetailsTopic(int Id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var result = _topicService.Get(Id);
            var topic = _topicService.GetOne(Id);
            var topicResponses = _topicResponseService.GetAllResponsesInTopic(Id);
            var addResponse = new AddTopicResponseDTO
            {
                Topic = topic,
                Owner = currentUser,
                Description = string.Empty,
                Image = null

            };

            var currentUserId = _userManager.GetUserId(User);
            //bool isCurrentUserInterested = interestedUsers.Any(u => u.User.Id == currentUserId);

            ViewBag.CurrentUserId = currentUserId;
            if (result.Value != null)
            {
                bool isOwner = currentUserId == result.Value.Publisher.Id;
                ViewBag.isOwner = isOwner;
                var viewModel = new TopicDetailsViewModel
                {
                    TopicInfo = result.Value,
                    Responses = topicResponses,
                    ResponseToAdd = addResponse
                };
                return View(viewModel);
            }
            else if (result.Error == TopicErrorCode.TopicNotFound)
            {
                TempData["ErrorMessage"] = "Topic not found";
                return RedirectToAction("Error", "Home");
            }
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddResponse(AddTopicResponseDTO model)
        {
            var currentUser = await _userManager.FindByIdAsync(model.Owner.Id);
            model.Owner = currentUser;
            if (model.Image?.Base64 == "defaultBase64Value" && model.Image?.Extension == "defaultExtension")
            {
                model.Image = null;
            }

            var isBanned = _banService.GetActiveBan(currentUser.Id);
            if (isBanned != null)
            {
                TempData["ErrorMessage"] = "You are blocked you cannot actually plan new event.";
                return RedirectToAction("Error", "Home");
            }
            _topicResponseService.Add(model);
            return RedirectToAction("DetailsTopic", new { Id = model.Topic.Id });
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var comment = _topicResponseService.GetOne(commentId);

            if (comment == null)
            {
                TempData["ErrorMessage"] = "Comment not found.";
                return RedirectToAction("Error", "Home");
            }

            if (comment.Owner.Id != currentUser.Id && !User.IsInRole("Admin"))
            {
                TempData["ErrorMessage"] = "You are not authorized to delete this comment.";
                return RedirectToAction("Error", "Home");
            }

            _topicResponseService.Delete(commentId);

            return RedirectToAction("DetailsTopic", new { Id = comment.Topic.Id }); 
        }


    }
}
