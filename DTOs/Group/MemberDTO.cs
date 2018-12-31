using hcm.Database.Models;
using hcm.DTOs.Users;

namespace hcm.DTOs.Groups
{
    public class MemberDTO
    {
        public bool IsGroupAdmin { get; set; }

        public UserDTO User { get; set; }

        public MemberDTO(GroupMembership m)
        {
            IsGroupAdmin = m.IsGroupAdmin;
            User = new UserDTO(m.User);
        }
    }
}