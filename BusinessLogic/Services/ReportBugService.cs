using BusinessLogic.DTO.Bug;
using BusinessLogic.Errors;
using BusinessLogic.Repositories;
using BusinessLogic.Repositories.Interfaces;
using BusinessLogic.Services.Interfaces;
using BusinessLogic.Services.Response;
using DB.Entities;

namespace BusinessLogic.Services
{
    public class ReportBugService : IReportBugService
    {
        private readonly IBugReportRepository _bugReportRepository;
        public ReportBugService(IBugReportRepository bugReportRepository)
        {
            _bugReportRepository = bugReportRepository;
        }

        public List<GetReportBugDTO> GetAll()
        {
            return _bugReportRepository.GetAll().Select(CreateReportBugDTO).ToList();
        }

        public Result<int?, BugReportErrorCode> ReportBug(ReportBugDTO reportBug)
        {
            var bugReport = CreateNewBugReport(reportBug);
            _bugReportRepository.Add(bugReport);
            return bugReport.Id;
        }

        public BugReportErrorCode? Resolve(int id)
        {
            var dbEntity = _bugReportRepository.GetOne(id);
            if (dbEntity == null)
                return BugReportErrorCode.EntityNotFound;
            dbEntity.BugState = DB.Enums.BugState.Resolved;
            _bugReportRepository.Update(dbEntity);
            return null;
        }

        private BugReport CreateNewBugReport(ReportBugDTO reportBug) =>
            new(reportBug.Reporter, reportBug.Description, DateTime.Now, reportBug.Image, reportBug.BugType, DB.Enums.BugState.New);

        private GetReportBugDTO CreateReportBugDTO(BugReport bugReport) =>
            new(bugReport.Id, bugReport.Reporter, bugReport.Description, bugReport.Image, bugReport.BugType, bugReport.BugState);
    }
}
