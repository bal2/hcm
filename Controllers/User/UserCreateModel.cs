using System.ComponentModel.DataAnnotations;

namespace hcm.Controllers.Users
{
    public class UserCreateModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Phone { get; set; }
    }
}