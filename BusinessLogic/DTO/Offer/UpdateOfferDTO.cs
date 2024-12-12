using Entities = DB.Entities;

namespace BusinessLogic.DTO.Offer
{
    public class UpdateOfferDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public bool IsReserved { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Price { get; set; }
        public List<Entities.File> Images { get; set; }

        public UpdateOfferDTO() { }
        public UpdateOfferDTO(int id, string description, string location, bool isReserved, string email, string phone, int price, List<Entities.File> images)
        {
            Id = id;
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
