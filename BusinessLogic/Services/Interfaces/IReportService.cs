using BusinessLogic.DTO.Report;
using BusinessLogic.Errors;
using BusinessLogic.Services.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Interfaces
{
    public interface IReportService
    {
        Result<int?, ReportErrorCode> ReportUser(ReportUserDTO report);
    }
}
