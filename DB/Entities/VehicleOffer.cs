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
        public VehicleOfferState State {  get; set; } 

        public VehicleOffer() { }

        public VehicleOffer(
            int id, string description, DateTime creationTime, string location, User user, bool isReserved, string email, string phone, int price,
            string brand, string model, string generation, string version, TransmissionType transmission, VehicleDriveType drive, BodyType body,
            string color, VehicleCondition condition, int numberOfSeats, int yearOfProduction, int mileage, OwnerType firstOwner, string vin, List<File> images, VehicleOfferState state)
            : base(id, description, creationTime, location, user, isReserved, email, phone, price, images)
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
            State = state;
        }

        public VehicleOffer(
            string description, DateTime creationTime, string location, User user, bool isReserved, string email, string phone, int price,
            string brand, string model, string generation, string version, TransmissionType transmission, VehicleDriveType drive, BodyType body,
            string color, VehicleCondition condition, int numberOfSeats, int yearOfProduction, int mileage, OwnerType firstOwner, string vin, List<File> images, VehicleOfferState state)
            : base(description, creationTime, location, user, isReserved, email, phone, price, images)
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
            State = state;
        }
    }
}
