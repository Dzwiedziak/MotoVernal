using DB.Entities;

namespace BusinessLogic.Repositories.Interfaces
{
    public interface ITopicResponseReactionRepository
    {
        void Add(TopicResponseReaction entity);
        void Delete(TopicResponseReaction entity);
        TopicResponseReaction? FindWhereUserAndTopicResponse(User user, TopicResponse topicResponse);
        TopicResponseReaction? Get(int id);
        void Update(TopicResponseReaction entity);
        List<TopicResponseReaction> GetAll();
    }
}
