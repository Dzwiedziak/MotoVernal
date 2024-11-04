using Entities = DB.Entities;

namespace BusinessLogic.DTO.TopicResponse
{
    public class GetTopicResponseDTO
    {
        public Entities.Topic Topic { get; set; }
        public Entities.User Owner { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }

        public GetTopicResponseDTO(Entities.Topic topic, Entities.User owner, string description, DateTime creationTime)
        {
            Topic = topic;
            Owner = owner;
            Description = description;
            CreationTime = creationTime;
        }
    }
}
