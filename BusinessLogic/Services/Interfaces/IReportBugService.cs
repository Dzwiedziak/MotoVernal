using BusinessLogic.DTO.Bug;
using BusinessLogic.Errors;
using BusinessLogic.Services.Response;
using DB.Entities;

namespace BusinessLogic.Services.Interfaces
{
    public interface IReportBugService
    {
        Result<int?, BugReportErrorCode> ReportBug(ReportBugDTO reportBug);
        List<GetReportBugDTO> GetAll();
        BugReportErrorCode? Resolve(int id);
    }
}
