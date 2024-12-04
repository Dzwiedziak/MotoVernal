using BusinessLogic.DTO.VehicleOffer;
using BusinessLogic.Errors;
using BusinessLogic.Repositories.Interfaces;
using BusinessLogic.Services.Interfaces;
using BusinessLogic.Services.Response;
using DB.Entities;

namespace BusinessLogic.Services
{
    public class VehicleOfferService : IVehicleOfferService
    {
        private readonly IVehicleOfferRepository _vehicleOfferRepository;
        private readonly IUserService _userService;
        public VehicleOfferService(IVehicleOfferRepository vehicleOfferRepository,
                                   IUserService userService)
        {
            _vehicleOfferRepository = vehicleOfferRepository;
            _userService = userService;
        }

        public Result<int?, VehicleOfferErrorCode> Add(AddVehicleOfferDTO vehicleOffer)
        {
            List<VehicleOffer> dbVehicleOffers = _vehicleOfferRepository.GetAll();
            if (FindVehicleWithSameVIN(vehicleOffer.VIN, dbVehicleOffers) is not null)
                return VehicleOfferErrorCode.VehicleWithVinExists;

            VehicleOffer newVehicleOffer = CreateVehicleOffer(vehicleOffer);
            _vehicleOfferRepository.Add(newVehicleOffer);
            return newVehicleOffer.Id;
        }

        public Result<GetVehicleOfferDTO?, VehicleOfferErrorCode> Get(int id)
        {
            VehicleOffer? dbVehicleOffer = _vehicleOfferRepository.GetOne(id);
            if (dbVehicleOffer is null)
                return VehicleOfferErrorCode.VehicleNotFound;

            return CreateGetVehicleOfferDTO(dbVehicleOffer);
        }

        public Result<List<GetVehicleOfferDTO>, UserErrorCode> GetUserVehicleOffers(string userID)
        {
            if (!_userService.CheckExistance(userID))
                return UserErrorCode.UserNotFound;
            return _vehicleOfferRepository.GetAll()
                   .Where(dbOffer => String.Equals(dbOffer.User.Id, userID))
                   .Select(offer => CreateGetVehicleOfferDTO(offer))
                   .ToList();
        }

        public VehicleOfferErrorCode? Update(int id, UpdateVehicleOfferDTO vehicleOffer)
        {
            VehicleOffer? dbVehicleOffer = _vehicleOfferRepository.GetOne(id);
            if (dbVehicleOffer is null)
                return VehicleOfferErrorCode.VehicleNotFound;

            List<VehicleOffer> dbVehicleOffers = _vehicleOfferRepository.GetAll();
            if (dbVehicleOffer.VIN != vehicleOffer.VIN &&
                FindVehicleWithSameVIN(dbVehicleOffer.VIN, dbVehicleOffers) is not null)
            {
                return VehicleOfferErrorCode.VehicleWithVinExists;
            }
            UpdateVehicleOffer(ref dbVehicleOffer, vehicleOffer);
            _vehicleOfferRepository.Update(dbVehicleOffer);
            return null;
        }

        private VehicleOffer? FindVehicleWithSameVIN(string vin, List<VehicleOffer> vehicleOffers) =>
            vehicleOffers.FirstOrDefault(vehicleoffer => String.Equals(vehicleoffer.VIN, vin));


        private VehicleOffer CreateVehicleOffer(AddVehicleOfferDTO vehicleOffer)
        {
            return new VehicleOffer(
                description: vehicleOffer.Description,
                creationTime: DateTime.Now,
                location: vehicleOffer.Location,
                user: vehicleOffer.User,
                isReserved: false,
                email: vehicleOffer.Email,
                phone: vehicleOffer.Phone,
                price: vehicleOffer.Price,
                brand: vehicleOffer.Brand,
                model: vehicleOffer.Model,
                generation: vehicleOffer.Generation,
                version: vehicleOffer.Version,
                transmission: vehicleOffer.Transmission,
                drive: vehicleOffer.Drive,
                body: vehicleOffer.Body,
                color: vehicleOffer.Color,
                condition: vehicleOffer.Condition,
                numberOfSeats: vehicleOffer.NumberOfSeats,
                yearOfProduction: vehicleOffer.YearOfProduction,
                mileage: vehicleOffer.Mileage,
                firstOwner: vehicleOffer.FirstOwner,
                vin: vehicleOffer.VIN,
                images: vehicleOffer.Images
            );
        }
        private GetVehicleOfferDTO CreateGetVehicleOfferDTO(VehicleOffer vehicleOffer)
        {
            return new GetVehicleOfferDTO(
                id: vehicleOffer.Id,
                description: vehicleOffer.Description,
                creationTime: DateTime.Now,
                location: vehicleOffer.Location,
                user: vehicleOffer.User,
                isReserved: false,
                email: vehicleOffer.Email,
                phone: vehicleOffer.Phone,
                price: vehicleOffer.Price,

                brand: vehicleOffer.Brand,
                model: vehicleOffer.Model,
                generation: vehicleOffer.Generation,
                version: vehicleOffer.Version,
                transmission: vehicleOffer.Transmission,
                drive: vehicleOffer.Drive,
                body: vehicleOffer.Body,
                color: vehicleOffer.Color,
                condition: vehicleOffer.Condition,
                numberOfSeats: vehicleOffer.NumberOfSeats,
                yearOfProduction: vehicleOffer.YearOfProduction,
                mileage: vehicleOffer.Mileage,
                firstOwner: vehicleOffer.FirstOwner,
                vIN: vehicleOffer.VIN,
                images: vehicleOffer.Images
            );
        }
        private void UpdateVehicleOffer(ref VehicleOffer oldVehicleOffer, UpdateVehicleOfferDTO vehicleOffer)
        {
            oldVehicleOffer.Brand = vehicleOffer.Brand;
            oldVehicleOffer.Model = vehicleOffer.Model;
            oldVehicleOffer.Generation = vehicleOffer.Generation;
            oldVehicleOffer.Version = vehicleOffer.Version;
            oldVehicleOffer.Transmission = vehicleOffer.Transmission;
            oldVehicleOffer.Drive = vehicleOffer.Drive;
            oldVehicleOffer.Body = vehicleOffer.Body;
            oldVehicleOffer.Color = vehicleOffer.Color;
            oldVehicleOffer.Condition = vehicleOffer.Condition;
            oldVehicleOffer.NumberOfSeats = vehicleOffer.NumberOfSeats;
            oldVehicleOffer.YearOfProduction = vehicleOffer.YearOfProduction;
            oldVehicleOffer.Mileage = vehicleOffer.Mileage;
            oldVehicleOffer.FirstOwner = vehicleOffer.FirstOwner;
            oldVehicleOffer.VIN = vehicleOffer.VIN;
            oldVehicleOffer.Images = vehicleOffer.Images;
        }
    }
}
