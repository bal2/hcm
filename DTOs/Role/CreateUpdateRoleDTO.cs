using System;
using hcm.Database.Models;

namespace hcm.DTOs.Roles
{
    public class CreateUpdateRoleDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}