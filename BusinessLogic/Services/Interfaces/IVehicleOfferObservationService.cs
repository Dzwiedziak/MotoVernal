using BusinessLogic.DTO.VehicleOfferObservation;
using BusinessLogic.Errors;
using BusinessLogic.Services.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Interfaces
{
    public interface IVehicleOfferObservationService
    {
        Result<int, VehicleOfferObservationErrorCode> AddVehicleOfferObservation(AddVehicleOfferObservationDTO vehicleOfferObservation);
        VehicleOfferObservationErrorCode? DeleteVehicleOfferObservation(int id);
    }
}
