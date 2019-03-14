using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace hcm.Auth
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == CustomClaimTypes.Permissions))
                return Task.CompletedTask;

            var permissions = new List<string>(context.User.FindFirst(claim => claim.Type == CustomClaimTypes.Permissions).Value.Split(" "));

            bool hasPermission = permissions.Exists(p => p == requirement.RequiredPermission);

            if (hasPermission)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }

    }
}