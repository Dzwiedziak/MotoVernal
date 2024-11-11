using BusinessLogic.DTO.Bug;
using BusinessLogic.Errors;
using BusinessLogic.Repositories;
using BusinessLogic.Services.Interfaces;
using BusinessLogic.Services.Response;
using DB;
using DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class ReportBugService : IReportBugService
    {
        private readonly BugReportRepository _bugReportRepository;
        public ReportBugService(BugReportRepository bugReportRepository)
        {
            _bugReportRepository = bugReportRepository;
        }

        public Result<int?, BugReportErrorCode> ReportBug(ReportBugDTO reportBug)
        {
            var bugReport = CreateNewBugReport(reportBug);
            _bugReportRepository.Add(bugReport);
            return bugReport.Id;
        }

        private BugReport CreateNewBugReport(ReportBugDTO reportBug) =>
            new(reportBug.Reporter, reportBug.Description, DateTime.Now, reportBug.Image);
    }
}
