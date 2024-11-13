using BusinessLogic.Repositories.Interfaces;
using DB;
using DB.Entities;

namespace BusinessLogic.Repositories
{
    public class BugReportRepository : IBugReportRepository
    {
        private readonly MVAppContext _context;
        public BugReportRepository(MVAppContext context)
        {
            _context = context;
        }
        public void Add(BugReport bugReport) { _context.Add(bugReport); _context.SaveChanges(); }

        public List<BugReport> GetAll() => _context.BugReports.ToList();

        public BugReport? GetOne(int id) => _context.BugReports.FirstOrDefault(x => x.Id == id);

        public void Update(BugReport bugReport) { _context.Update(bugReport); _context.SaveChanges(); }
    }
}
