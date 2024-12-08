using System.ComponentModel.DataAnnotations;
using Entites = DB.Entities;

namespace BusinessLogic.DTO.Report
{
    public class ReportUserDTO
    {
        [Required(ErrorMessage = "Reporter user is required.")]
        public Entites.User Reporter { get; set; }
        [Required(ErrorMessage = "Reported user is required.")]
        public Entites.User Reported { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Reason must be between 10 and 100 characters.")]
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
