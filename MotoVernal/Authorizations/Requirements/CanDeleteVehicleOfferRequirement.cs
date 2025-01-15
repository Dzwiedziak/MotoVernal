using Microsoft.AspNetCore.Authorization;

namespace MotoVernal.Authorizations.Requirements
{
    public class CanDeleteVehicleOfferRequirement : IAuthorizationRequirement
    {
        public int VehicleOfferId { get; set; }

        public CanDeleteVehicleOfferRequirement(int vehicleOfferId)
        {
            VehicleOfferId = vehicleOfferId;
        }
    }
}
