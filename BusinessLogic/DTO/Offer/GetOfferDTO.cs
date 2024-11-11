using DB.Entities;
using Entities = DB.Entities;

namespace BusinessLogic.DTO.Offer
{
    public class GetOfferDTO
    {
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public Entities.Location Location { get; set; }
        public Entities.User User { get; set; }
        public bool IsReserved { get; set; }
        public string Email { get; set; }
        public Entities.Phone Phone { get; set; }
        public Entities.Price Price { get; set; }
        public List<Entities.File> Images { get; set; }

        public GetOfferDTO(string description, DateTime creationTime, Location location, Entities.User user, bool isReserved, string email, Phone phone, Price price, List<Entities.File> images)
        {
            Description = description;
            CreationTime = creationTime;
            Location = location;
            User = user;
            IsReserved = isReserved;
            Email = email;
            Phone = phone;
            Price = price;
            Images = images;
        }
    }
}
