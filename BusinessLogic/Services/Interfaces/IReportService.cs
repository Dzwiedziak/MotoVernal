using BusinessLogic.DTO.Report;
using BusinessLogic.Errors;
using BusinessLogic.Services.Response;
using DB.Entities;

namespace BusinessLogic.Services.Interfaces
{
    public interface IReportService
    {
        Result<int?, ReportErrorCode> ReportUser(ReportUserDTO report);

        List<Report> GetAllReports();
        Report GetReportById(int id);
        List<Report> GetReporterReports(string userId);
        void RejectReport(int id);
    }
}
