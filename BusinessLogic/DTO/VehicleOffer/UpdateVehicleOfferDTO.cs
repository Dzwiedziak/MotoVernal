using BusinessLogic.DTO.Offer;
using DB.Entities;
using Entities = DB.Entities;
using DB.Enums;
using System.Collections.Generic;
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

        public UpdateVehicleOfferDTO(string brand, string model, string generation, string version, TransmissionType transmission, VehicleDriveType drive, BodyType body, string color, VehicleCondition condition, int numberOfSeats, int yearOfProduction, int mileage, OwnerType firstOwner, string vIN, string description, Location location, bool isReserved, string email, Phone phone, Price price, List<Entities.File> images)
            : base(description, location, isReserved, email, phone, price, images)
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
