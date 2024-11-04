using BusinessLogic.DTO.Bug;
using BusinessLogic.Errors;
using BusinessLogic.Services.Response;
using DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Interfaces
{
    public interface IReportBugService
    {
        Result<int?, BugReportErrorCode> ReportBug(ReportBugDTO reportBug);
    }
}
