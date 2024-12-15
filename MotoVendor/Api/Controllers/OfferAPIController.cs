using BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MotoVendor.Api.DTO;

namespace MotoVendor.Api.Controllers
{
    [Route("api/offers")]
    public class OfferAPIController : ControllerBase
    {
        readonly IVehicleOfferService _vehicleOfferService;

        public OfferAPIController(IVehicleOfferService vehicleOfferService)
        {
            _vehicleOfferService = vehicleOfferService;
        }

        [HttpPatch("{id}/isreserved")]
        public IActionResult PatchReservation(int id, [FromBody] ReservationOfferDTO dto)
        {
            var error = _vehicleOfferService.UpdateReservation(id, dto.isReserved);
            switch (error)
            {
                case null:
                    return Ok();
                case BusinessLogic.Errors.VehicleOfferErrorCode.VehicleNotFound:
                    return NoContent();
                default:
                    return StatusCode(500, "Unknown error has occured");
            }
        }
    }
}
