using Microsoft.AspNetCore.Authorization;

namespace MotoVendor.Authorizations.Requirements
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
