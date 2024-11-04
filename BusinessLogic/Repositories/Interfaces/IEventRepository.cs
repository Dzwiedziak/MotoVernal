using DB.Entities;

namespace BusinessLogic.Repositories.Interfaces
{
    public interface IEventRepository
    {
        List<Event> GetAll();
        Event? GetOne(int id);
        void Add(Event user);
        void Update(Event user);
    }
}
