using System;
using System.ComponentModel.DataAnnotations;
using hcm.Database.Models;

namespace hcm.Controllers.Users
{
    public class UserUpdateResourceModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }
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
        public bool IsPictureApproved { get; set; }
        public string CardId { get; set; }

        public UserUpdateResourceModel() { }
    }
}