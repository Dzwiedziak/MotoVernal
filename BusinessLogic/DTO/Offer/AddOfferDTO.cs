using Entities = DB.Entities;

namespace BusinessLogic.DTO.Offer
{
    public class AddOfferDTO
    {
        public string Description { get; set; }
        public string Location { get; set; }
        //public Entities.Location Location { get; set; }
        public Entities.User User { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        //public Entities.Phone Phone { get; set; }
        public int Price { get; set; }
        public List<Entities.File> Images { get; set; }

        public AddOfferDTO()
        {
        }

        public AddOfferDTO(string description, string location, Entities.User user, string email, string phone, int price, List<Entities.File> images)
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
