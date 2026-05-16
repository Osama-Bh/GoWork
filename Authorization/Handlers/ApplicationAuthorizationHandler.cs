using GoWork.Authorization.Operations;
using GoWork.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Security.Claims;

namespace GoWork.Authorization.Handlers
{
    public class ApplicationAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Application>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Application resource)
        {
            var claims = context.User.FindFirst(ClaimTypes.NameIdentifier);
            if (claims == null || !int.TryParse(claims.Value, out int id))
            {
                return Task.CompletedTask;
            }

            switch (requirement.Name)
            {
                
                case nameof(ApplicationOperations.Withdraw):
                    if (context.User.IsInRole("Candidate") && resource.Seeker?.UserId == id)
                    {
                        context.Succeed(requirement);
                    }
                    break;
            }

            return Task.CompletedTask;
        }
    }
}
