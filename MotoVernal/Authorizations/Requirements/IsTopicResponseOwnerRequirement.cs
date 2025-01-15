using Microsoft.AspNetCore.Authorization;

namespace MotoVernal.Authorizations.Requirements
{
    public class IsTopicResponseOwnerRequirement : IAuthorizationRequirement
    {
        public int Id { get; set; }

        public IsTopicResponseOwnerRequirement(int id)
        {
            Id = id;
        }
    }
}
