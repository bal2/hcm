using Microsoft.AspNetCore.Authorization;

namespace hcm.Auth
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public string RequiredPermission { get; private set; }

        public PermissionRequirement(string permission)
        {
            this.RequiredPermission = permission;
        }
    }
}