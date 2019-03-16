using System.ComponentModel.DataAnnotations;
using hcm.DTOs.Roles;

namespace hcm.Controllers.Roles
{
    public class RoleCreateResourceModel
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

    }
}