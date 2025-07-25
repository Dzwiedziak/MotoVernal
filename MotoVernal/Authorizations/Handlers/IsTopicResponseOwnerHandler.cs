﻿using BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using MotoVernal.Authorizations.Requirements;
using System.Security.Claims;

namespace MotoVernal.Authorizations.Handlers
{
    public class IsTopicResponseOwnerHandler : AuthorizationHandler<IsTopicResponseOwnerRequirement>
    {
        readonly ITopicResponseService _topicResponseService;

        public IsTopicResponseOwnerHandler(ITopicResponseService topicResponseService)
        {
            _topicResponseService = topicResponseService;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, IsTopicResponseOwnerRequirement requirement)
        {
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return;
            var topicResponse = _topicResponseService.Get(requirement.Id);
            if (!topicResponse.IsSuccess)
            {
                return;
            }
            if (topicResponse.Value.Owner.Id == userId)
            {
                context.Succeed(requirement);
            }
        }
    }
}
