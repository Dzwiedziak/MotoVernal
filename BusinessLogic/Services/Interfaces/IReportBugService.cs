using BusinessLogic.DTO.Bug;
using BusinessLogic.Errors;
using BusinessLogic.Services.Response;

namespace BusinessLogic.Services.Interfaces
{
    public interface IReportBugService
    {
        Result<int?, BugReportErrorCode> ReportBug(ReportBugDTO reportBug);
    }
}
