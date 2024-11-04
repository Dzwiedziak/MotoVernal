using DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repositories.Interfaces
{
    public interface IReportRepository
    {
        List<Report> GetAll();
        Report? GetOne(int id);
        void Add(Report report);
        void Update(Report report);
    }
}
