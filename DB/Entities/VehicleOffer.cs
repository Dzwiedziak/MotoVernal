using DB.Enums;

namespace DB.Entities
{
    public class VehicleOffer : Offer
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

        public VehicleOffer() { }
        public VehicleOffer(VehicleOffer vehicleOffer) : base(
            vehicleOffer.Id,
            vehicleOffer.Description,
            vehicleOffer.CreationTime,
            vehicleOffer.Location,
            vehicleOffer.User,
            vehicleOffer.IsReserved,
            vehicleOffer.Email,
            vehicleOffer.Phone,
            vehicleOffer.Price)
        {
            Brand = vehicleOffer.Brand;
            Model = vehicleOffer.Model;
            Generation = vehicleOffer.Generation;
            Version = vehicleOffer.Version;
            Transmission = vehicleOffer.Transmission;
            Drive = vehicleOffer.Drive;
            Body = vehicleOffer.Body;
            Color = vehicleOffer.Color;
            Condition = vehicleOffer.Condition;
            NumberOfSeats = vehicleOffer.NumberOfSeats;
            YearOfProduction = vehicleOffer.YearOfProduction;
            Mileage = vehicleOffer.Mileage;
            FirstOwner = vehicleOffer.FirstOwner;
            VIN = vehicleOffer.VIN;
        }

        public VehicleOffer(
            int id, string description, DateTime creationTime, Location location, User user, bool isReserved, string email, Phone phone, Price price,
            string brand, string model, string generation, string version, TransmissionType transmission, VehicleDriveType drive, BodyType body,
            string color, VehicleCondition condition, int numberOfSeats, int yearOfProduction, int mileage, OwnerType firstOwner, string vin)
            : base(id, description, creationTime, location, user, isReserved, email, phone, price)
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
            VIN = vin;
        }

        public VehicleOffer(
            string description, DateTime creationTime, Location location, User user, bool isReserved, string email, Phone phone, Price price,
            string brand, string model, string generation, string version, TransmissionType transmission, VehicleDriveType drive, BodyType body,
            string color, VehicleCondition condition, int numberOfSeats, int yearOfProduction, int mileage, OwnerType firstOwner, string vin)
            : base(description, creationTime, location, user, isReserved, email, phone, price)
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
            VIN = vin;
        }
    }
}
