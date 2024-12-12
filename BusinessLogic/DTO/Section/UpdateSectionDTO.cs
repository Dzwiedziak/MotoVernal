using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Entities = DB.Entities;

namespace BusinessLogic.DTO.Section
{
    public class UpdateSectionDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 100 characters.")]
        public string Title { get; set; }
        public Entities.Section Parent { get; set; }
        public Entities.File? Image { get; set; }

        public UpdateSectionDTO() { }
        public UpdateSectionDTO(int id, string title, Entities.Section parent, Entities.File? image)
        {
            Id = id;
            Title = title;
            Parent = parent;
            Image = image;
        }
    }
}
