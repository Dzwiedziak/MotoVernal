using Entities = DB.Entities;

namespace BusinessLogic.DTO.Ban
{
    public class BanUserDTO
    {
        public Entities.User Banned { get; set; }
        public Entities.User Banner { get; set; }
        public DateTime ExpirationTime { get; set; }
        public string Reason { get; set; }

        public BanUserDTO(Entities.User banned, Entities.User banner, DateTime creationTime, DateTime expirationTime, string reason)
        {
            Banned = banned;
            Banner = banner;
            ExpirationTime = expirationTime;
            Reason = reason;
        }
    }
}
