using System;
using hcm.Database.Models;

namespace hcm.DTOs.Roles
{
    public class PermissionDTO
    {
        public long PermissionId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public PermissionDTO(Permission p)
        {
            PermissionId = p.PermissionId;
            Name = p.Name;
            Description = p.Description;
        }
    }
}