using System.ComponentModel.DataAnnotations;
using Entities = DB.Entities;

namespace BusinessLogic.DTO.Topic
{
    public class AddTopicDTO
    {
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 50 characters.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Descripiton is required.")]
        [StringLength(300, MinimumLength = 3, ErrorMessage = "Descripiton  must be between 3 and 300 characters.")]
        public string Description { get; set; }
        public Entities.User Publisher { get; set; }
        public Entities.Section Section { get; set; }
        public Entities.File? Image { get; set; }
        public List<Entities.TopicResponse>? Responses { get; set; }

        public AddTopicDTO() { }
        public AddTopicDTO(string title, string description, Entities.User publisher, Entities.Section section, Entities.File? image, List<Entities.TopicResponse> responses)
        {
            Title = title;
            Description = description;
            Publisher = publisher;
            Section = section;
            Image = image;
            Responses = responses;
        }
    }
}
