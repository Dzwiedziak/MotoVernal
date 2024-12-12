using BusinessLogic.DTO.VehicleOfferObservation;
using BusinessLogic.Errors;
using BusinessLogic.Services.Response;

namespace BusinessLogic.Services.Interfaces
{
    public interface IVehicleOfferObservationService
    {
        Result<int, VehicleOfferObservationErrorCode> AddVehicleOfferObservation(AddVehicleOfferObservationDTO vehicleOfferObservation);
        VehicleOfferObservationErrorCode? DeleteVehicleOfferObservation(int id);
    }
}
