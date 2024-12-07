using BusinessLogic.Repositories.Interfaces;
using DB;
using DB.Entities;
using Microsoft.EntityFrameworkCore;

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

        public List<Report> GetAll() => _context.Reports
            .Include(r => r.Reporter)
            .Include(r => r.Reported)
            .Include(r => r.Reported.ProfileImage)
            .Include(r => r.Reporter.ProfileImage)
            .ToList();

        public Report? GetOne(int id) => _context.Reports
            .Include(r => r.Reporter)
            .Include(r => r.Reported)
            .Include(r => r.Reported.ProfileImage)
            .Include(r => r.Reporter.ProfileImage)
            .Include(r => r.Image)
            .FirstOrDefault(r => r.Id == id);

        public void Update(Report report) { _context.Update(report); _context.SaveChanges(); }

        public void Delete(int id)
        {
            var report = _context.Reports.FirstOrDefault(r => r.Id == id);
            if (report != null)
            {
                _context.Reports.Remove(report);
                _context.SaveChanges();
            }
        }

    }
}
