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
        public string Address { get; set; }
        [RegularExpression("^[0-9]*$")]
        public string Zip { get; set; }
        public string Town { get; set; }
        [RegularExpression("^[0-9]*$")]
        public string Phone { get; set; }
    }
}