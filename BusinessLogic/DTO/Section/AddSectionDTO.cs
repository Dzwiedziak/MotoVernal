using System.ComponentModel.DataAnnotations;
using Entities = DB.Entities;

namespace BusinessLogic.DTO.Section
{
    public class AddSectionDTO
    {
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 25 characters.")]
        public string Title { get; set; }
        public Entities.Section Parent { get; set; }
        public Entities.File? Image { get; set; }

        public AddSectionDTO() { }
        public AddSectionDTO(string title, Entities.Section parent, Entities.File? image)
        {
            Title = title;
            Parent = parent;
            Image = image;
        }
    }
}
