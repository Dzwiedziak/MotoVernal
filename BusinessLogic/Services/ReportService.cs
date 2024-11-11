using BusinessLogic.DTO.Report;
using BusinessLogic.Errors;
using BusinessLogic.Repositories;
using BusinessLogic.Repositories.Interfaces;
using BusinessLogic.Services.Interfaces;
using BusinessLogic.Services.Response;
using DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class ReportService : IReportService
    {
        private readonly ReportRepository _reportRepository;

        public ReportService(ReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public Result<int?, ReportErrorCode> ReportUser(ReportUserDTO report)
        {
            var newReport = CreateNewReport(report);
            _reportRepository.Add(newReport);
            return newReport.Id;
        }

        private Report CreateNewReport(ReportUserDTO report) =>
            new(report.Reporter, report.Reported, report.Description, DateTime.Now, report.Image);
    }
}
