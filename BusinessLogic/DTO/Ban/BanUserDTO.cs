using BusinessLogic.Decorators;
using System.ComponentModel.DataAnnotations;
using Entities = DB.Entities;

namespace BusinessLogic.DTO.Ban
{
    public class BanUserDTO 
    {
        [Required(ErrorMessage = "Banned user is required.")]
        public Entities.User Banned { get; set; }
        [Required(ErrorMessage = "Banner user is required.")]
        public Entities.User Banner { get; set; }
        [Required]
        [FutureDate(ErrorMessage = "Expiration time must be in the future.")]
        public DateTime ExpirationTime { get; set; }
        [Required(ErrorMessage = "Reason is required.")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Reason must be between 5 and 200 characters.")]
        public string Reason { get; set; }
        public Entities.File? Image { get; set; }

        public BanUserDTO() { }
        public BanUserDTO(Entities.User banned, Entities.User banner, DateTime expirationTime, string reason, Entities.File? image)
        {
            Banned = banned;
            Banner = banner;
            ExpirationTime = expirationTime;
            Reason = reason;
            Image = image;
        }
    }
}
