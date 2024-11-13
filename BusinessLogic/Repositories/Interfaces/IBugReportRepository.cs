using DB.Entities;

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
