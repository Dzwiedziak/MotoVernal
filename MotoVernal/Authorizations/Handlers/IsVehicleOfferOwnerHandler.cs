using BusinessLogic.Services.Interfaces;
using DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using MotoVernal.Authorizations.Requirements;
using System.Security.Claims;

namespace MotoVernal.Authorizations.Handlers
{
    public class IsVehicleOfferOwnerHandler : AuthorizationHandler<IsVehicleOfferOwnerRequirement>
    {
        readonly MVAppContext _context;
        readonly IUserService _userService;
        public IsVehicleOfferOwnerHandler(MVAppContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, IsVehicleOfferOwnerRequirement requirement)
        {
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return;
            var vehicleOffer = await _context.VehicleOffers.Include(vo => vo.User).FirstOrDefaultAsync(vo => vo.Id == requirement.VehicleOfferId);
            if (vehicleOffer != null && vehicleOffer.User.Id == userId)
            {
                context.Succeed(requirement);
            }
        }
    }
}
