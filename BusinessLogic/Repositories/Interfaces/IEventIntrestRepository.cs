using DB.Entities;

namespace BusinessLogic.Repositories.Interfaces
{
    public interface IEventIntrestRepository
    {
        List<EventInterest> GetAll();
        List<EventInterest> GetAllByEventId(int eventId);
        EventInterest? GetOne(int id);
        EventInterest? GetOne(string userId, int eventId);
        void Add(EventInterest eventInterest);
        void Delete(int id);

    }
}
