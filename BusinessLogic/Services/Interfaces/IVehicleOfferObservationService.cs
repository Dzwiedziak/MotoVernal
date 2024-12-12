using BusinessLogic.DTO.VehicleOfferObservation;
using BusinessLogic.Errors;
using BusinessLogic.Services.Response;
using DB.Entities;

namespace BusinessLogic.Services.Interfaces
{
    public interface IVehicleOfferObservationService
    {
        Result<int, VehicleOfferObservationErrorCode> Add(AddVehicleOfferObservationDTO entityAddDTO);
        VehicleOfferObservationErrorCode? Delete(int id);
        VehicleOfferObservation? FindByUserAndOffer(string userId, int offerId);
    }
}
