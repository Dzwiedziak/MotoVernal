using DB.Entities;
using Entities = DB.Entities;

namespace BusinessLogic.DTO.Offer
{
    public class UpdateOfferDTO
    {
        public string Description { get; set; }
        public Entities.Location Location { get; set; }
        public bool IsReserved { get; set; }
        public string Email { get; set; }
        public Entities.Phone Phone { get; set; }
        public Entities.Price Price { get; set; }
        public List<Entities.File> Images { get; set; }

        public UpdateOfferDTO(string description, Location location, bool isReserved, string email, Phone phone, Price price, List<Entities.File> images)
        {
            Description = description;
            Location = location;
            IsReserved = isReserved;
            Email = email;
            Phone = phone;
            Price = price;
            Images = images;
        }
    }
}
