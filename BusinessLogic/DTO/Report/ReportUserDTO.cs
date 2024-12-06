using Entites = DB.Entities;

namespace BusinessLogic.DTO.Report
{
    public class ReportUserDTO
    {
        public Entites.User Reporter { get; set; }
        public Entites.User Reported { get; set; }
        public string Description { get; set; }
        public Entites.File? Image { get; set; }

        public ReportUserDTO() { }
        public ReportUserDTO(Entites.User reporter, Entites.User reported, string description, Entites.File? image)
        {
            Reporter = reporter;
            Reported = reported;
            Description = description;
            Image = image;
        }
    }
}
