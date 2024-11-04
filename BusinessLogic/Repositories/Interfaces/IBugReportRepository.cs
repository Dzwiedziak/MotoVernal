using DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repositories.Interfaces
{
    public interface IBugReportRepository
    {
        List<BugReport> GetAll();
        BugReport? GetOne(int id);
        void Add(BugReport bugReport);
        void Update(BugReport bugReport);
    }
}
