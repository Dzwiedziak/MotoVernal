using BusinessLogic.DTO.VehicleOfferObservation;
using BusinessLogic.Errors;
using BusinessLogic.Repositories.Interfaces;
using BusinessLogic.Services.Interfaces;
using BusinessLogic.Services.Response;
using DB.Entities;

namespace BusinessLogic.Services
{
    public class VehicleOfferObservationService : IVehicleOfferObservationService
    {
        readonly IVehicleOfferObservationRepository _vehicleOfferObservationRepository;

        public VehicleOfferObservationService(IVehicleOfferObservationRepository vehicleOfferObservationRepository)
        {
            _vehicleOfferObservationRepository = vehicleOfferObservationRepository;
        }

        public Result<int, VehicleOfferObservationErrorCode> AddVehicleOfferObservation(AddVehicleOfferObservationDTO vehicleOfferObservation)
        {
            
        }

        public VehicleOfferObservationErrorCode? DeleteVehicleOfferObservation(int id)
        {
            throw new NotImplementedException();
        }

        public VehicleOfferObservation Find(int userId, int offerId)
        {
            throw new NotImplementedException();
        }
    }
}
