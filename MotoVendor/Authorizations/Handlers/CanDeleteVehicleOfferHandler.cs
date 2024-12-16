using BusinessLogic.Services;
using BusinessLogic.Services.Interfaces;
using DB.Entities;
using Microsoft.AspNetCore.Authorization;
using MotoVendor.Authorizations.Requirements;
using System.Security.Claims;

namespace MotoVendor.Authorizations.Handlers
{
    public class CanDeleteVehicleOfferHandler : AuthorizationHandler<CanDeleteVehicleOfferRequirement>
    {
        readonly IVehicleOfferService _vehicleOfferService;

        public CanDeleteVehicleOfferHandler(IVehicleOfferService vehicleOfferService)
        {
            _vehicleOfferService = vehicleOfferService;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CanDeleteVehicleOfferRequirement requirement)
        {
            if (context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
                return;
            }

            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return;
            }

            var result = _vehicleOfferService.Get(requirement.VehicleOfferId);
            if (!result.IsSuccess || result.Value == null)
            {
                return;
            }

            if (result.Value.User.Id == userId)
            {
                context.Succeed(requirement);
            }
        }
    }
}
