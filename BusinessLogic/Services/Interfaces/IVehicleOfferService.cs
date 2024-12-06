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
        VehicleOfferErrorCode? Update(int id, UpdateVehicleOfferDTO vehicleOffer);
    }
}
