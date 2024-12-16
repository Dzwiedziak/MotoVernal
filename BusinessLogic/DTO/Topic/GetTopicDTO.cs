using Entities = DB.Entities;

namespace BusinessLogic.DTO.Topic
{
    public class GetTopicDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public Entities.User Publisher { get; set; }
        public Entities.Section Section { get; set; }
        public Entities.File? Image { get; set; }
        public List<Entities.TopicResponse>? Responses { get; set; }

        public GetTopicDTO(int id, string title, string description, DateTime creationTime, Entities.User publisher, Entities.Section section, Entities.File? image, List<Entities.TopicResponse>? responses)
        {
            Id = id;
            Title = title;
            Description = description;
            CreationTime = creationTime;
            Publisher = publisher;
            Section = section;
            Image = image;
            Responses = responses;
        }
    }
}
