using Entities = DB.Entities;

namespace BusinessLogic.DTO.Topic
{
    public class UpdateTopicDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Entities.File? Image { get; set; }

        public UpdateTopicDTO(string title, string description, Entities.File? image)
        {
            Title = title;
            Description = description;
            Image = image;
        }
    }
}
