using BusinessLogic.DTO.Event;
using BusinessLogic.DTO.Section;
using BusinessLogic.Services.Interfaces;
using BusinessLogic.Services.Response;
using DB.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MotoVendor.ViewModels;

namespace MotoVendor.Controllers
{
    public class ForumController : Controller
    {
        private readonly ISectionService _sectionService;
        private readonly ITopicService _topicService;
        private readonly IBanService _banService;
        private readonly UserManager<User> _userManager;

        public ForumController(ISectionService sectionService, ITopicService topicService, IBanService banService, UserManager<User> userManager)
        {
            _sectionService = sectionService;
            _topicService = topicService;
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
        public IActionResult EditSection()
        {
            return View();
        }
        public IActionResult AddTopic()
        {
            return View();
        }
        public IActionResult EditTopic()
        {
            return View();
        }
        public IActionResult DetailsTopic()
        {
            return View();
        }
    }
}
