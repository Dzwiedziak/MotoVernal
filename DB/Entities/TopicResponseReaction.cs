using DB.Enums;

namespace DB.Entities
{
    public class TopicResponseReaction
    {
        public int Id { get; set; }
        public User User { get; set; }
        public TopicResponse TopicResponse { get; set; }
        public ReactionType ReactionType { get; set; }

        public TopicResponseReaction() { }

        public TopicResponseReaction(int id, User user, TopicResponse topicResponse, ReactionType reactionType)
        {
            Id = id;
            User = user;
            TopicResponse = topicResponse;
            ReactionType = reactionType;
        }
    }
}
