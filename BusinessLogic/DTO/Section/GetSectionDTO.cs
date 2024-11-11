using Entities = DB.Entities;

namespace BusinessLogic.DTO.Section
{
    public class GetSectionDTO
    {
        public string Title { get; set; }
        public Entities.Section? Parent { get; set; }
        public Entities.File? Image { get; set; }

        public GetSectionDTO(string title, Entities.Section? parent, Entities.File? image)
        {
            Title = title;
            Parent = parent;
            Image = image;
        }
    }
}
