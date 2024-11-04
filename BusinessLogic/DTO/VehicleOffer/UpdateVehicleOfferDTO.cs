using BusinessLogic.DTO.Offer;
using DB.Enums;
namespace BusinessLogic.DTO.VehicleOffer
{
    public class UpdateVehicleOfferDTO : UpdateOfferDTO
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Generation { get; set; }
        public string Version { get; set; }
        public TransmissionType Transmission { get; set; }
        public VehicleDriveType Drive { get; set; }
        public BodyType Body { get; set; }
        public string Color { get; set; }
        public VehicleCondition Condition { get; set; }
        public int NumberOfSeats { get; set; }
        public int YearOfProduction { get; set; }
        public int Mileage { get; set; }
        public OwnerType FirstOwner { get; set; }
        public string VIN { get; set; }
    }
}
