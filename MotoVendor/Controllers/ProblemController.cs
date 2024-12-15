using BusinessLogic.DTO.Bug;
using BusinessLogic.Errors;
using BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MotoVendor.Controllers
{
    public class ProblemController : Controller
    {
        readonly IReportBugService _reportBugService;

        public ProblemController(IReportBugService reportBugService)
        {
            _reportBugService = reportBugService;
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
