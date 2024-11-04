using BusinessLogic.Repositories.Interfaces;
using DB;
using DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly MVAppContext _context;

        public ReportRepository(MVAppContext context)
        {
            _context = context;
        }

        public void Add(Report report) { _context.Add(report); _context.SaveChanges(); }

        public List<Report> GetAll() => _context.Reports.ToList();

        public Report? GetOne(int id) => _context.Reports.FirstOrDefault(r => r.Id == id);

        public void Update(Report report) { _context.Update(report); _context.SaveChanges(); }
    }
}
