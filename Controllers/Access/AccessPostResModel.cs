using System.ComponentModel.DataAnnotations;

namespace hcm.Controllers.Access
{
    public class AccessPostResourceController
    {
        [Required]
        public string CardId { get; set; }
    }
}