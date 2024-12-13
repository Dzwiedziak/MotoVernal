using BusinessLogic.DTO.API;
using BusinessLogic.DTO.VehicleOfferObservation;
using BusinessLogic.Errors;
using BusinessLogic.Services.Interfaces;
using DB.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MotoVendor.Api.Controllers
{
    [Route("api/offers/observations")]
    [ApiController]
    public class OfferObservationAPIController : ControllerBase
    {
        readonly IVehicleOfferObservationService _service;
        public OfferObservationAPIController(IVehicleOfferObservationService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Add(AddVehicleOfferObservationDTO offerObservation)
        {
            var result = _service.Add(offerObservation);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            switch (result.Error)
            {
                case VehicleOfferObservationErrorCode.RelationAlreadyExists:
                    return StatusCode(409, "User is already observing this offer");
                default:
                    return StatusCode(500, "Unknown error has occured");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var error = _service.Delete(id);
            switch (error)
            {
                case null:
                    return Ok();
                case VehicleOfferObservationErrorCode.RelationNotExists:
                    return StatusCode(409, "User is already not observing this offer");
                default:
                    return StatusCode(500, "Unknown error has occured");
            }
        }
    }
}
