using BusinessLogic.Repositories.Interfaces;
using DB;
using DB.Entities;

namespace BusinessLogic.Repositories
{
    public class TopicResponseReactionRepository : ITopicResponseReactionRepository
    {
        readonly MVAppContext _context;

        public TopicResponseReactionRepository(MVAppContext context)
        {
            _context = context;
        }

        public void Add(TopicResponseReaction entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(TopicResponseReaction entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public TopicResponseReaction? FindWhereUserAndTopicResponse(User user, TopicResponse topicResponse)
        {
            return _context.TopicResponseReactions.Where(r => r.User.Id == user.Id && r.TopicResponse.Id == topicResponse.Id).FirstOrDefault();
        }

        public TopicResponseReaction? Get(int id)
        {
            return _context.TopicResponseReactions.Where(r => r.Id == id).FirstOrDefault();
        }

        public void Update(TopicResponseReaction entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }
    }
}
