using Entities = DB.Entities;

namespace BusinessLogic.DTO.Topic
{
    public class AddTopicDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Entities.User Publisher { get; set; }
        public Entities.Section Section { get; set; }
        public Entities.File? Image { get; set; }

        public AddTopicDTO(string title, string description, Entities.User publisher, Entities.Section section, Entities.File? image)
        {
            Title = title;
            Description = description;
            Publisher = publisher;
            Section = section;
            Image = image;
        }
    }
}
