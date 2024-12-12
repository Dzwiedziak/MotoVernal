using BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.DTO.VehicleOfferObservation;
using BusinessLogic.Errors;

namespace MotoVendor.Controllers.Api
{
    [Route("api/vehicleoffers/observations")]
    [ApiController]
    public class OfferObservationAPIController : ControllerBase
    {
        readonly IVehicleOfferObservationService _vehicleOfferObservationService;

        public OfferObservationAPIController(IVehicleOfferObservationService vehicleOfferObservationService)
        {
            _vehicleOfferObservationService = vehicleOfferObservationService;
        }

        public IActionResult AddOfferObservation(AddVehicleOfferObservationDTO vehicleOfferObservation)
        {
            var result = _vehicleOfferObservationService.AddVehicleOfferObservation(vehicleOfferObservation);
            if(result.IsSuccess)
            {
                return Ok(result.Value);
            }
            switch(result.Error)
            {
                case VehicleOfferObservationErrorCode.RelationAlreadyExists:
                    return StatusCode(409, "User is already observing this offer");
                default:
                    return StatusCode(500, "Unknown error has occured");
            }
        }
        public IActionResult DeleteUserObservation(int id)
        {
            var error = _vehicleOfferObservationService.DeleteVehicleOfferObservation(id);
            if(error == null)
            {
                return Ok();
            }
            switch(error)
            {
                case VehicleOfferObservationErrorCode.RelationNotExists:
                    return StatusCode(409, "User is already not observing this offer");
                default:
                    return StatusCode(500, "Unknown error has occured");
            }
        }
    }
}
