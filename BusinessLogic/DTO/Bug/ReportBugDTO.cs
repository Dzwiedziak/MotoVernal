using Entities = DB.Entities;

namespace BusinessLogic.DTO.Bug
{
    public class ReportBugDTO
    {
        public Entities.User Reporter { get; set; }
        public string Description { get; set; }
        public Entities.File? Image { get; set; }

        public ReportBugDTO(Entities.User reporter, string description, Entities.File? image)
        {
            Reporter = reporter;
            Description = description;
            Image = image;
        }
    }
}
