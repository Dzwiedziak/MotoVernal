using BusinessLogic.DTO.Report;
using BusinessLogic.Errors;
using BusinessLogic.Services.Response;

namespace BusinessLogic.Services.Interfaces
{
    public interface IReportService
    {
        Result<int?, ReportErrorCode> ReportUser(ReportUserDTO report);
    }
}
