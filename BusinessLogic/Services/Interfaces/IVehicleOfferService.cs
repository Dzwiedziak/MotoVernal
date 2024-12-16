using BusinessLogic.DTO.VehicleOffer;
using BusinessLogic.Errors;
using BusinessLogic.Services.Response;

namespace BusinessLogic.Services.Interfaces
{
    public interface IVehicleOfferService
    {
        Result<int?, VehicleOfferErrorCode> Add(AddVehicleOfferDTO vehicleOffer);
        Result<GetVehicleOfferDTO?, VehicleOfferErrorCode> Get(int id);
        List<GetVehicleOfferDTO> GetAll();
        List<GetVehicleOfferDTO> GetAllOwners(string userId);
        VehicleOfferErrorCode? Update(UpdateVehicleOfferDTO vehicleOffer);
        UpdateVehicleOfferDTO? GetUpdateDTO(int id);
        VehicleOfferErrorCode? UpdateReservation(int id, bool isReserved);
        bool IsObservedBy(string userId, int offerId);
        VehicleOfferErrorCode? Delete(int id);
    }
}
