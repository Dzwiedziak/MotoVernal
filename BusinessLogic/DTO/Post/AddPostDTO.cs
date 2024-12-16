using System.ComponentModel.DataAnnotations;
using Entities = DB.Entities;

namespace BusinessLogic.DTO.Post
{
    public class AddPostDTO
    {
        [Required(ErrorMessage = "Publisher is required.")]
        public Entities.User Publisher { get; set; }
        [Required(ErrorMessage = "Content is required.")]
        [StringLength(300, MinimumLength = 1, ErrorMessage = "Content must be between 1 and 300 characters.")]
        public string Content { get; set; }
        public Entities.File? Image { get; set; }

        public AddPostDTO() { }
        public AddPostDTO(Entities.User publisher, string content, Entities.File? image)
        {
            Publisher = publisher;
            Content = content;
            Image = image;
        }
    }
}
