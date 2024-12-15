using DB.Enums;

namespace BusinessLogic.DTO.TopicResponseReaction
{
    public class AddTopicResponseReactionDTO
    {
        public string UserId { get; set; }
        public int TopicResponseId { get; set; }
        public ReactionType ReactionType { get; set; }

        public AddTopicResponseReactionDTO() { }

        public AddTopicResponseReactionDTO(string userId, int topicResponseId, ReactionType reactionType)
        {
            UserId = userId;
            TopicResponseId = topicResponseId;
            ReactionType = reactionType;
        }
    }
}
