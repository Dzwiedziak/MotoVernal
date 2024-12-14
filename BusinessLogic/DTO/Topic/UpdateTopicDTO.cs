using System.ComponentModel.DataAnnotations;
using Entities = DB.Entities;

namespace BusinessLogic.DTO.Topic
{
    public class UpdateTopicDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 50 characters.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Descripiton is required.")]
        [StringLength(300, MinimumLength = 3, ErrorMessage = "Descripiton  must be between 3 and 300 characters.")]
        public string Description { get; set; }
        public Entities.File? Image { get; set; }
        public UpdateTopicDTO() { }
        public UpdateTopicDTO(int id,string title, string description, Entities.File? image)
        {
            Id = id;
            Title = title;
            Description = description;
            Image = image;
        }
    }
}
