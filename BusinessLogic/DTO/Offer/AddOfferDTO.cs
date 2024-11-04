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
    }
}
