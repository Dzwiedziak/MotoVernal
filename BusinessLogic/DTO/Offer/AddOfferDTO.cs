using DB.Entities;
using Entities = DB.Entities;

namespace BusinessLogic.DTO.Offer
{
    public class AddOfferDTO
    {
        public string Description { get; set; }
        public Entities.Location Location { get; set; }
        public Entities.User User { get; set; }
        public string Email { get; set; }
        public Entities.Phone Phone { get; set; }
        public Entities.Price Price { get; set; }
        public List<Entities.File> Images { get; set; }

        public AddOfferDTO(string description, Location location, Entities.User user, string email, Phone phone, Price price, List<Entities.File> images)
        {
            Description = description;
            Location = location;
            User = user;
            Email = email;
            Phone = phone;
            Price = price;
            Images = images;
        }
    }
}
