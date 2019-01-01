using hcm.Database.Models;
using hcm.DTOs.Users;

namespace hcm.DTOs.Groups
{
    public class MemberDetailsDTO
    {
        public bool IsGroupAdmin { get; set; }

        public UserDetailsDTO User { get; set; }

        public MemberDetailsDTO(GroupMembership m)
        {
            IsGroupAdmin = m.IsGroupAdmin;
            User = new UserDetailsDTO(m.User);
        }
    }
}