using BusinessLogic.Repositories.Interfaces;
using DB;
using DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly MVAppContext _context;

        public EventRepository(MVAppContext context)
        {
            _context = context;
        }

        public List<Event> GetAll() => _context.Events
            .Include(e => e.Publisher)
            .Include(e => e.Publisher.ProfileImage)
            .Include(e => e.Image)
            .ToList();
        public Event? GetOne(int id) => _context.Events
            .Include(e => e.Publisher)
            .Include(e => e.Publisher.ProfileImage)
            .Include(e => e.Image)
            .FirstOrDefault(e => e.Id == id);

        public void Add(Event eventEntity) { _context.Events.Add(eventEntity); _context.SaveChanges(); }
        public void Update(Event eventEntity) { _context.Events.Update(eventEntity); _context.SaveChanges(); }
    }
}
