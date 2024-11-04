using Entites = DB.Entities;

namespace BusinessLogic.DTO.Report
{
    public class ReportUserDTO
    {
        public Entites.User Reporter { get; set; }
        public Entites.User Reported { get; set; }
        public string Description { get; set; }
    }
}
