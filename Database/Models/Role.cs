using System;
using System.Collections.Generic;

namespace hcm.Database.Models
{
    public class Role
    {
        public long RoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<RolePermission> Permissions { get; set; }
        public List<RoleUser> Users { get; set; }
    }
}