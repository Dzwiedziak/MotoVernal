using BusinessLogic.DTO.EventInterest;
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

        public void Add(AddEventInterestDTO eventInterest)
        {
            var newEventInterest = CreateEventInterest(eventInterest);
            _context.Add(newEventInterest);
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

        public EventInterest CreateEventInterest(AddEventInterestDTO eventInterest) =>
            new(eventInterest.User, eventInterest.Event);
    }
}
