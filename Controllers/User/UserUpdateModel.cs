using System;
using hcm.Database.Models;

namespace hcm.Controllers.Users
{
    public class UserUpdateResourceModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Phone { get; set; }
        public bool IsPictureApproved { get; set; }

        public UserUpdateResourceModel() { }
    }
}