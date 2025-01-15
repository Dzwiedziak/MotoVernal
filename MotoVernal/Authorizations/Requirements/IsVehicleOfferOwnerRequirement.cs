using Microsoft.AspNetCore.Authorization;

namespace MotoVernal.Authorizations.Requirements
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
