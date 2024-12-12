using BusinessLogic.DTO.Event;
using BusinessLogic.DTO.Section;
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
            /*var parentSection = _sectionService.GetOne(sectionToEdit.Parent);
            if (parentSection == null)
            {
                TempData["ErrorMessage"] = "Parent section not found";
                return RedirectToAction("Error", "Home");
            }*/
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
