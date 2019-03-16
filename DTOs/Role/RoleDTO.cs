using System;
using hcm.Database.Models;

namespace hcm.DTOs.Roles
{
    public class RoleDTO
    {
        public long RoleId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public RoleDTO(Role r)
        {
            RoleId = r.RoleId;
            Name = r.Name;
            Description = r.Description;
        }
    }
}