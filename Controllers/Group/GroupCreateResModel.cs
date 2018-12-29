using System.ComponentModel.DataAnnotations;

namespace hcm.Controllers.Groups {
    public class GroupCreateResourceModel {

        [Required]
        public string Name { get; set; }

        [Required]
        public string ShortName { get; set; }

        public string Description { get; set; }
    }
}