﻿using BusinessLogic.Decorators;
using BusinessLogic.DTO.Offer;
using DB.Enums;
using Entities = DB.Entities;

namespace BusinessLogic.DTO.VehicleOffer
{
    public class GetVehicleOfferDTO : GetOfferDTO
    {
        [Filterable]
        [Sortable]
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Generation { get; set; }
        public string Version { get; set; }
        public TransmissionType Transmission { get; set; }
        public VehicleDriveType Drive { get; set; }
        public BodyType Body { get; set; }
        public string Color { get; set; }
        [Filterable]
        [Sortable]
        public VehicleCondition Condition { get; set; }
        public int NumberOfSeats { get; set; }
        public int YearOfProduction { get; set; }
        [Filterable]
        [Sortable]
        public int Mileage { get; set; }
        public OwnerType FirstOwner { get; set; }
        public string VIN { get; set; }

        public GetVehicleOfferDTO(int id, string description, DateTime creationTime, string location, DB.Entities.User user, bool isReserved, string email, string phone, int price, string brand, string model, string generation, string version, TransmissionType transmission, VehicleDriveType drive, BodyType body, string color, VehicleCondition condition, int numberOfSeats, int yearOfProduction, int mileage, OwnerType firstOwner, string vIN, List<Entities.File> images)
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
            VIN = vIN;
        }
    }
}
