using Entities = DB.Entities;

namespace BusinessLogic.DTO.Section
{
    public class AddSectionDTO
    {
        public string Title { get; set; }
        public Entities.Section Parent { get; set; }
    }
}
