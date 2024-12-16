using BusinessLogic.DTO.Offer;
using BusinessLogic.ValidationAttributes;
using DB.Enums;
using System.ComponentModel.DataAnnotations;
using Entities = DB.Entities;
namespace BusinessLogic.DTO.VehicleOffer
{
    public class UpdateVehicleOfferDTO : UpdateOfferDTO
    {
        [Required(ErrorMessage = "Brand is required.")]
        [StringLength(20, ErrorMessage = "Brand cannot be longer than 20 characters.")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Model is required.")]
        [StringLength(20, ErrorMessage = "Model cannot be longer than 20 characters.")]
        public string Model { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Generation cannot be longer than 20 characters.")]
        public string Generation { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Version cannot be longer than 20 characters.")]
        public string Version { get; set; }

        [Required(ErrorMessage = "Transmission type is required.")]
        public TransmissionType Transmission { get; set; }

        [Required(ErrorMessage = "Drive type is required.")]
        public VehicleDriveType Drive { get; set; }

        [Required(ErrorMessage = "Body type is required.")]
        public BodyType Body { get; set; }

        [StringLength(30, ErrorMessage = "Color cannot be longer than 20 characters.")]
        public string Color { get; set; }

        [Required(ErrorMessage = "Condition is required.")]
        public VehicleCondition Condition { get; set; }

        [Range(1, 100, ErrorMessage = "Number of seats must be between 1 and 100.")]
        public int NumberOfSeats { get; set; }

        [YearOfProduction(1886, ErrorMessage = "Year of production must be between 1886 and the current year.")]
        public int YearOfProduction { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Mileage cannot be negative.")]
        public int Mileage { get; set; }

        [Required(ErrorMessage = "First owner type is required.")]
        public OwnerType FirstOwner { get; set; }

        [Required(ErrorMessage = "VIN is required.")]
        [StringLength(17, ErrorMessage = "VIN must be 17 characters.")]
        public string VIN { get; set; }

        public UpdateVehicleOfferDTO() { }

        public UpdateVehicleOfferDTO(string brand, string model, string generation, string version, TransmissionType transmission, VehicleDriveType drive, BodyType body, string color, VehicleCondition condition, int numberOfSeats, int yearOfProduction, int mileage, OwnerType firstOwner, string vIN, int id, string description, string location, bool isReserved, string email, string phone, int price, List<Entities.File> images)
            : base(id, description, location, isReserved, email, phone, price, images)
        {
            Brand = brand;
            Model = model;
            Generation = generation;
            Version = version;
            Transmission = transmission;
            Drive = drive;
            Body = body;
            Color = color;
            Condition = condition;
            NumberOfSeats = numberOfSeats;
            YearOfProduction = yearOfProduction;
            Mileage = mileage;
            FirstOwner = firstOwner;
            VIN = vIN;
        }
    }
}
