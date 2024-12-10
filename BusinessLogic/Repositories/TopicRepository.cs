using BusinessLogic.Repositories.Interfaces;
using DB;
using DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Repositories
{
    public class TopicRepository : ITopicRepository
    {
        private readonly MVAppContext _context;

        public TopicRepository(MVAppContext context)
        {
            _context = context;
        }

        public List<Topic> GetAll() => _context.Topics.ToList();
        public List<Topic> GetAllInSection(int sectionId) => _context.Topics
                .Where(t => t.Section.Id == sectionId).
                 Include(t => t.Publisher)  
                .Include(t => t.Section)    
                .Include(t => t.Image)    
                .ToList();
        public Topic? GetOne(int id) => _context.Topics.FirstOrDefault(t => t.Id == id);
        public void Add(Topic topic) { _context.Topics.Add(topic); _context.SaveChanges(); }
        public void Update(Topic topic) { _context.Topics.Update(topic); _context.SaveChanges(); }
    }
}
