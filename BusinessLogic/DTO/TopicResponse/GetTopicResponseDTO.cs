using Entities = DB.Entities;

namespace BusinessLogic.DTO.TopicResponse
{
    public class GetTopicResponseDTO
    {
        public int Id { get; set; }
        public Entities.Topic Topic { get; set; }
        public Entities.User Owner { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public Entities.File? Image { get; set; }

        public GetTopicResponseDTO(int id,Entities.Topic topic, Entities.User owner, string description, DateTime creationTime, Entities.File? image)
        {
            Id = id;
            Topic = topic;
            Owner = owner;
            Description = description;
            CreationTime = creationTime;
            Image = image;
        }
    }
}
