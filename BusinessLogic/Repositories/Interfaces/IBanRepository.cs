using DB.Entities;

namespace BusinessLogic.Repositories.Interfaces
{
    public interface IBanRepository
    {
        List<Ban> GetAll();
        Ban? GetOne(int id);
        void Add(Ban ban);
        void Update(Ban ban);
    }
}
