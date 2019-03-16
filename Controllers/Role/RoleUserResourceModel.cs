using hcm.DTOs.Users;

namespace hcm.Controllers.Roles
{
    public class RoleUserResourceModel
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }

        public RoleUserResourceModel(UserDTO u)
        {
            UserId = u.UserId;
            FirstName = u.FirstName;
            LastName = u.LastName;
            Title = u.Title;
        }
    }
}