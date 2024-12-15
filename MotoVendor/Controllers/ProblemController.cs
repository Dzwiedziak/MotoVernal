using BusinessLogic.DTO.Bug;
using BusinessLogic.Errors;
using BusinessLogic.Services.Interfaces;
using DB.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MotoVendor.Controllers
{
    public class ProblemController : Controller
    {
        readonly IReportBugService _reportBugService;
        readonly UserManager<User> _userManager;

        public ProblemController(IReportBugService reportBugService, UserManager<User> userManager)
        {
            _reportBugService = reportBugService;
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Report()
        {
            var model = new ReportBugDTO();
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Report(ReportBugDTO addDTO)
        {
            var user = _userManager.GetUserAsync(User).Result!;
            addDTO.Reporter = user;
            var result = _reportBugService.ReportBug(addDTO);
            if (result.IsSuccess)
            {
                return RedirectToAction("IntroductionPage", "Home");
            }
            TempData["ErrorMessage"] = "Unknown error happened";
            return RedirectToAction("Error", "Home");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult BugReports()
        {
            var bugReports = _reportBugService.GetAll();
            var notResolved = bugReports.Where(r => r.BugState != DB.Enums.BugState.Resolved).ToList();
            return View(notResolved);
        }

        [Authorize(Roles = "Admin")]
        [Route("{id}")]
        public IActionResult ResolveReport(int id)
        {
            var result = _reportBugService.Resolve(id);
            switch (result)
            {
                case null:
                    return RedirectToAction("BugReports");
                default:
                    TempData["ErrorMessage"] = "Resolving bug failed";
                    return RedirectToAction("Error", "Home");
            }
        }

    }
}
