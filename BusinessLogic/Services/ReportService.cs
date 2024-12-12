using BusinessLogic.DTO.Report;
using BusinessLogic.Errors;
using BusinessLogic.Repositories.Interfaces;
using BusinessLogic.Services.Interfaces;
using BusinessLogic.Services.Response;
using DB.Entities;

namespace BusinessLogic.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;

        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public Result<int?, ReportErrorCode> ReportUser(ReportUserDTO report)
        {
            var newReport = CreateNewReport(report);
            _reportRepository.Add(newReport);
            return newReport.Id;
        }
        public List<Report> GetAllReports()
        {
            return _reportRepository.GetAll().ToList();
        }
        public Report GetReportById(int id)
        {
            return _reportRepository.GetOne(id);
        }
        public List<Report> GetReporterReports(string userId)
        {
            return _reportRepository.GetAll()
                .Where(r => r.Reporter.Id == userId).ToList();
        }
        public void RejectReport(int id)
        {
            _reportRepository.Delete(id);
        }
        private Report CreateNewReport(ReportUserDTO report) =>
            new(report.Reporter, report.Reported, report.Description, DateTime.Now, report.Image);
    }
}
