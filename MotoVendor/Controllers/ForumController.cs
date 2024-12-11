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
            if (sectionId == null)
            {
                var sectionMain = _sectionService.Get(1);
                var childSectionsMain = _sectionService.GetChildrenSections(1);
                var topicSectionsMain = _topicService.GetAllInSections(1);
                var vm = new SectionsAndTopicsListViewModel
                {
                    SectionInfo = sectionMain.Value,
                    ChildSections = childSectionsMain.Value,
                    TopicsList = topicSectionsMain

                };
                return View(vm);
            }
            var section = _sectionService.Get(sectionId.Value);
            if(section.Value == null)
            {
                TempData["ErrorMessage"] = "Section of this id not exist";
                return RedirectToAction("Error", "Home");
            }
            var childSections = _sectionService.GetChildrenSections(sectionId.Value);
            var topicSections = _topicService.GetAllInSections(sectionId.Value);
            var viewModel = new SectionsAndTopicsListViewModel
            {
                SectionInfo = section.Value,
                ChildSections = childSections.Value,
                TopicsList = topicSections
            };
            return View(viewModel);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult AddSection()
        {
            return View();
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
