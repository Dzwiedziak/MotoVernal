using System.ComponentModel.DataAnnotations;
using Entities = DB.Entities;

namespace BusinessLogic.DTO.Post
{
    public class UpdatePostDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Content is required.")]
        [StringLength(300, MinimumLength = 1, ErrorMessage = "Content must be between 1 and 300 characters.")]
        public string Content { get; set; }
        public Entities.File? Image { get; set; }

        public UpdatePostDTO() { }
        public UpdatePostDTO(int id, string content, Entities.File? image)
        {
            Id = id;
            Content = content;
            Image = image;
        }
    }
}
