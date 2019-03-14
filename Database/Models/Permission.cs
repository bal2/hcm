using System;
using System.Collections.Generic;

namespace hcm.Database.Models
{
    public class Permission
    {
        public long PermissionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<RolePermission> Roles { get; set; }
    }
}