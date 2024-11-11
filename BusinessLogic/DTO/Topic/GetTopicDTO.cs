using Entities = DB.Entities;

namespace BusinessLogic.DTO.Topic
{
    public class GetTopicDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public Entities.User Publisher { get; set; }
        public Entities.Section Section { get; set; }
        public Entities.File? Image { get; set; }

        public GetTopicDTO(string title, string description, DateTime creationTime, Entities.User publisher, Entities.Section section, Entities.File? image)
        {
            Title = title;
            Description = description;
            CreationTime = creationTime;
            Publisher = publisher;
            Section = section;
            Image = image;
        }
    }
}
