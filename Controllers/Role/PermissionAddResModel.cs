using System.ComponentModel.DataAnnotations;
using hcm.DTOs.Roles;

namespace hcm.Controllers.Roles
{
    public class PermissionAddResourceModel
    {
        [Required]
        public long PermissionId { get; set; }

    }
}