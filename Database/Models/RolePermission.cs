using System;
using System.Collections.Generic;

namespace hcm.Database.Models
{
    public class RolePermission
    {
        public long PermissionId { get; set; }
        public Permission Permission { get; set; }
        public long RoleId { get; set; }
        public Role Role { get; set; }
    }
}