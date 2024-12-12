using Microsoft.AspNetCore.Authorization;

namespace MotoVendor.Authorizations.Requirements
{
    public class IsVehicleOfferOwnerRequirement : IAuthorizationRequirement
    {
        public int VehicleOfferId { get; }
        public IsVehicleOfferOwnerRequirement(int vehicleOfferId)
        {
            VehicleOfferId = vehicleOfferId;
        }
    }
}
