using BusinessLogic.Repositories.Interfaces;
using DB;
using DB.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessLogic.Repositories
{
    public class TopicResponseRepository : ITopicResponseRepository
    {
        private readonly MVAppContext _context;

        public TopicResponseRepository(MVAppContext context)
        {
            _context = context;
        }

        public List<TopicResponse> GetAll() => _context.TopicResponses
            .Include(t => t.Owner)
            .Include(t => t.Owner.ProfileImage)
            .Include(t => t.Topic)
            .Include(t => t.Image)
            .ToList();
        public TopicResponse? GetOne(int id) => _context.TopicResponses
            .Include(r => r.Topic)
            .Include(r => r.Owner)
            .Include(r => r.Owner.ProfileImage)
            .FirstOrDefault(r => r.Id == id);
        public void Add(TopicResponse response) { _context.Attach(response.Topic); _context.TopicResponses.Add(response); _context.SaveChanges(); }
        public void Update(TopicResponse response) { _context.TopicResponses.Update(response); _context.SaveChanges(); }
        public void Delete(TopicResponse topicResponse)
        {
                _context.TopicResponses.Remove(topicResponse); 
                _context.SaveChanges();
        }
    }
}
