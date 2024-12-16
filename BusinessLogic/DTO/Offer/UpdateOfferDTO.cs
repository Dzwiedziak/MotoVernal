using System.ComponentModel.DataAnnotations;
using Entities = DB.Entities;

namespace BusinessLogic.DTO.Offer
{
    public class UpdateOfferDTO
    {
        public int Id { get; set; }
        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string Description { get; set; }

        [StringLength(100, ErrorMessage = "Location cannot be longer than 100 characters.")]
        public string Location { get; set; }
        public bool IsReserved { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Invalid phone number format.")]
        [StringLength(20, ErrorMessage = "Phone number cannot be longer than 20 characters.")]
        public string Phone { get; set; }


        [Range(0, int.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public int Price { get; set; }
        public List<Entities.File>? Images { get; set; }

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
