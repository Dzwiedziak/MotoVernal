using BusinessLogic.Repositories.Interfaces;
using DB;
using DB.Entities;

namespace BusinessLogic.Repositories
{
    public class EventInterestRepository : IEventIntrestRepository
    {
        private readonly MVAppContext _context;
        public EventInterestRepository(MVAppContext context)
        {
            _context = context;
        }

        public void Add(EventInterest eventInterest)
        {
            _context.Add(eventInterest);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var dbEventInterest = GetOne(id);
            if (dbEventInterest is not null)
                _context.EventInterests.Remove(dbEventInterest);
            _context.SaveChanges();
        }

        public List<EventInterest> GetAll()
        {
            return _context.EventInterests.ToList();
        }

        public EventInterest? GetOne(int id)
        {
            return _context.EventInterests.FirstOrDefault(e => e.Id == id);
        }

        public EventInterest? GetOne(string userId, int eventId)
        {
            return _context.EventInterests.FirstOrDefault(e => e.User.Id == userId && e.Event.Id == eventId);
        }


    }
}
