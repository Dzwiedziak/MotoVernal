using System.ComponentModel.DataAnnotations;
using Entities = DB.Entities;

namespace BusinessLogic.DTO.Ban
{
    public class BanUserDTO
    {
        public Entities.User Banned { get; set; }
        public Entities.User Banner { get; set; }
        [Required]
        public DateTime ExpirationTime { get; set; }
        [Required]
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
