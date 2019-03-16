using hcm.DTOs.Roles;

namespace hcm.Controllers.Roles
{
    public class PermissionResourceModel
    {
        public long PermissionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public PermissionResourceModel(PermissionDTO p)
        {
            PermissionId = p.PermissionId;
            Name = p.Name;
            Description = p.Description;
        }

    }
}