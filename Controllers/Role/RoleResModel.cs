using hcm.DTOs.Roles;

namespace hcm.Controllers.Roles
{
    public class RoleResourceModel
    {
        public long RoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public RoleResourceModel(RoleDTO r)
        {
            RoleId = r.RoleId;
            Name = r.Name;
            Description = r.Description;
        }
    }
}