using hcm.Database.Models;

namespace hcm.DTOs.Users
{
    public class UserDTO
    {
        public long UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }

        public UserDTO(User u)
        {
            UserId = u.UserId;
            Email = u.Email;
            FirstName = u.FirstName;
            LastName = u.LastName;
            Title = u.Title;
        }

    }
}