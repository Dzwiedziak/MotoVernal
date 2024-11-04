using Entities = DB.Entities;

namespace BusinessLogic.DTO.TopicResponse
{
    public class AddTopicResponseDTO
    {
        public Entities.Topic Topic { get; set; }
        public Entities.User Owner { get; set; }
        public string Description { get; set; }

        public AddTopicResponseDTO(Entities.Topic topic, Entities.User owner, string description)
        {
            Topic = topic;
            Owner = owner;
            Description = description;
        }
    }
}
