using System.ComponentModel.DataAnnotations;

namespace hcm.Controllers.Groups
{
    public class MemberCreateResourceModel
    {
        [Required]
        public long UserId { get; set; }

        public bool IsGroupAdmin { get; set; }
    }
}