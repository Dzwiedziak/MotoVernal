using DB.Enums;
using Entities = DB.Entities;

namespace BusinessLogic.DTO.Bug
{
    public class ReportBugDTO
    {
        public Entities.User Reporter { get; set; }
        public string Description { get; set; }
        public Entities.File? Image { get; set; }
        public BugType BugType { get; set; }

        public ReportBugDTO() { }

        public ReportBugDTO(Entities.User reporter, string description, Entities.File? image, BugType bugType, BugState bug)
        {
            Reporter = reporter;
            Description = description;
            Image = image;
            BugType = bugType;
        }
    }
}
