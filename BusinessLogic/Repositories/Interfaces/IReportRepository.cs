using DB.Entities;

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
