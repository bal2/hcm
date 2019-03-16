using System.ComponentModel.DataAnnotations;

namespace hcm.Controllers.Roles
{
    public class RoleUserAddResourceModel
    {
        [Required]
        public long UserId { get; set; }
    }
}