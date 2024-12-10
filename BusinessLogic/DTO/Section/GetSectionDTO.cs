using Entities = DB.Entities;

namespace BusinessLogic.DTO.Section
{
    public class GetSectionDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Entities.Section? Parent { get; set; }
        public Entities.File? Image { get; set; }

        public GetSectionDTO(int id, string title, Entities.Section? parent, Entities.File? image)
        {
            Id = id;
            Title = title;
            Parent = parent;
            Image = image;
        }
    }
}
