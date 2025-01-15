using BusinessLogic.DTO.Section;
using BusinessLogic.DTO.Topic;
using BusinessLogic.DTO.TopicResponse;
using BusinessLogic.Errors;
using BusinessLogic.Services.Interfaces;
using DB.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MotoVernal.Authorizations.Requirements;
using MotoVernal.ViewModels;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MotoVernal.Controllers
{
    public class ForumController : Controller
    {
        private readonly ISectionService _sectionService;
        private readonly ITopicService _topicService;
        private readonly ITopicResponseService _topicResponseService;
        private readonly IBanService _banService;
        private readonly UserManager<User> _userManager;
        private readonly IAuthorizationService _authorizationService;
        private readonly ITopicResponseReactionService _topicResponseReactionService;

        public ForumController(ISectionService sectionService, ITopicService topicService, ITopicResponseService topicResponseService, IBanService banService, UserManager<User> userManager, IAuthorizationService authorizationService, ITopicResponseReactionService topicResponseReactionService)
        {
            _sectionService = sectionService;
            _topicService = topicService;
            _topicResponseService = topicResponseService;
            _banService = banService;
            _userManager = userManager;
            _authorizationService = authorizationService;
            _topicResponseReactionService = topicResponseReactionService;
        }
        public async Task<IActionResult> SectionsAndTopicsList(int? sectionId, string sortBySection, string? searchSection,
                                                   string sortByTopic, string? searchTopic, DateTime? dateFrom, DateTime? dateTo,
                                                   bool? owner, bool? member)
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

            if (childSections.Value.Count() == 0)
            {
                ViewBag.NoChildrenSections = true;
            }
            else
            {
                ViewBag.NoChildrenSections = false;
            }
            if (!string.IsNullOrEmpty(searchSection))
            {
                childSections = childSections.Value.Where(section =>
                    !string.IsNullOrEmpty(section.Title) && section.Title.Contains(searchSection, StringComparison.OrdinalIgnoreCase)
                ).ToList();
            }
            switch (sortBySection)
            {
                case "alphabetical_asc":
                    childSections = childSections.Value.OrderBy(s => s.Title).ToList();
                    break;
                case "alphabetical_desc":
                    childSections = childSections.Value.OrderByDescending(s => s.Title).ToList();
                    break;
                default:
                    childSections = childSections.Value.OrderBy(s => s.Title).ToList();
                    break;
            }
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser != null)
            {
                if (owner.HasValue && owner.Value || member.HasValue && member.Value)
                {
                    topics = topics.Where(topic =>
                        currentUser.Id != null &&
                        (
                            owner.HasValue && owner.Value && topic.Publisher?.Id == currentUser.Id ||
                            member.HasValue && member.Value && topic.Responses.Any(comment => comment.Owner.Id == currentUser.Id)
                        )
                    ).ToList();
                }
            }
            if (!string.IsNullOrEmpty(searchTopic))
            {
                topics = topics.Where(topic =>
                    !string.IsNullOrEmpty(topic.Title) && topic.Title.Contains(searchTopic, StringComparison.OrdinalIgnoreCase)
                ).ToList();
            }
            if (dateFrom.HasValue)
            {
                topics = topics.Where(t => t.CreationTime >= dateFrom.Value).ToList();
            }
            if (dateTo.HasValue)
            {
                topics = topics.Where(t => t.CreationTime <= dateTo.Value).ToList();
            }
            switch (sortByTopic)
            {
                case "alphabetical_asc":
                    topics = topics.OrderBy(t => t.Title).ToList();
                    break;
                case "alphabetical_desc":
                    topics = topics.OrderByDescending(t => t.Title).ToList();
                    break;
                case "date_asc":
                    topics = topics.OrderBy(t => t.CreationTime).ToList();
                    break;
                case "date_desc":
                    topics = topics.OrderByDescending(t => t.CreationTime).ToList();
                    break;
                default:
                    topics = topics.OrderBy(t => t.CreationTime).ToList();
                    break;
            }
            var vm = new SectionsAndTopicsListViewModel
            {
                SectionInfo = sectionInfo.Value,
                ChildSections = childSections.Value,
                ParentSections = parentSections.Value,
                TopicsList = topics
            };
            ViewBag.SortBySection = sortBySection;
            ViewBag.SearchSection = searchSection;
            ViewBag.SortByTopic = sortByTopic;
            ViewBag.SearchTopic = searchTopic;
            ViewBag.DateFrom = dateFrom?.ToString("yyyy-MM-ddTHH:mm");
            ViewBag.DateTo = dateTo?.ToString("yyyy-MM-ddTHH:mm");
            ViewBag.Owner = owner;
            ViewBag.Member = member;

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
            if (sectionToEdit == null)
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
            if (result == null)
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

            if (sectionId == rootSectionResult.Value.Id)
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
                return RedirectToAction("DetailsTopic", new { model.Id });
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

            var user = await _userManager.GetUserAsync(User);
            ViewBag.CurrentUserId = user.Id;
            if (result.Value != null)
            {
                bool isOwner = user.Id == result.Value.Publisher.Id;
                ViewBag.isOwner = isOwner;
                foreach (var topicResponse in topicResponses)
                {
                    var reactionResult = _topicResponseReactionService.FindWhere(user.Id, topicResponse.Id);
                    if (!reactionResult.IsSuccess)
                    {
                        TempData["ErrorMessage"] = "References were not found";
                        return RedirectToAction("Error", "Home");
                    }
                    topicResponse.UserResponse = reactionResult.Value;
                    topicResponse.LikeCount = _topicResponseReactionService.GetLikeCount(topicResponse.Id);
                    topicResponse.DisLikeCount = _topicResponseReactionService.GetDisLikeCount(topicResponse.Id);
                }
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
            return RedirectToAction("DetailsTopic", new { model.Topic.Id });
        }
        [Authorize]
        [HttpPost]
        [Route("/EditResponse/{id}")]
        public async Task<IActionResult> EditResponse([FromRoute] int id, UpdateTopicResponseDTO model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var updateDTO = new UpdateTopicResponseDTO(model.Description, model.Image);
            if (currentUser == null)
            {
                return RedirectToAction("Error", "Home");
            }
            var permission = await _authorizationService.AuthorizeAsync(User, null, new IsTopicResponseOwnerRequirement(id));
            if (!permission.Succeeded)
            {
                return RedirectToAction("Error", "Home");
            }
            var result = _topicResponseService.Update(id, updateDTO);
            switch (result)
            {
                case null:
                    var topicResponse = _topicResponseService.GetOne(id);
                    var topic = _topicService.GetOne(topicResponse.Topic.Id);
                    return RedirectToAction("DetailsTopic", new { topic.Id });
                default:
                    return RedirectToAction("Error", "Home");
            }
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

            return RedirectToAction("DetailsTopic", new { comment.Topic.Id });
        }
    }
}
