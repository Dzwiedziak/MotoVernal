using Entities = DB.Entities;

namespace BusinessLogic.DTO.Section
{
    public class AddSectionDTO
    {
        public string Title { get; set; }
        public Entities.Section Parent { get; set; }
        public Entities.File? Image { get; set; }

        public AddSectionDTO(string title, Entities.Section parent, Entities.File? image)
        {
            Title = title;
            Parent = parent;
            Image = image;
        }
    }
}
