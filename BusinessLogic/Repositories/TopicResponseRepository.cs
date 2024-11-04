using BusinessLogic.Repositories.Interfaces;
using DB;
using DB.Entities;

namespace BusinessLogic.Repositories
{
    public class TopicResponseRepository : ITopicResponseRepository
    {
        private readonly MVAppContext _context;

        public TopicResponseRepository(MVAppContext context)
        {
            _context = context;
        }

        public List<TopicResponse> GetAll() => _context.TopicResponses.ToList();
        public TopicResponse? GetOne(int id) => _context.TopicResponses.FirstOrDefault(r => r.Id == id);
        public void Add(TopicResponse response) { _context.TopicResponses.Add(response); _context.SaveChanges(); }
        public void Update(TopicResponse response) { _context.TopicResponses.Update(response); _context.SaveChanges(); }
    }
}
