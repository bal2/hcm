using System.ComponentModel.DataAnnotations;
using hcm.DTOs.Roles;

namespace hcm.Controllers.Roles
{
    public class RoleUpdateResourceModel
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

    }
}