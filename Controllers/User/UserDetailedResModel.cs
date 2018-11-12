using System;
using hcm.Database.Models;

namespace hcm.Controllers.Users {
    public class UserDetailedResourceModel {
        public long UserId { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Phone { get; set; }
        public string Base64Picture { get; set; }
        public bool IsPictureApproved { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public UserDetailedResourceModel (User u) {
            this.UserId = u.UserId;
            this.Email = u.Email;
            this.FirstName = u.FirstName;
            this.LastName = u.LastName;
            this.Title = u.Title;
            this.Phone = u.Phone;
            this.IsPictureApproved = u.IsPictureApproved;
            this.Base64Picture = u.Picture != null ? Convert.ToBase64String(u.Picture) : null;
            this.CreatedAt = u.CreatedAt;
            this.UpdatedAt = u.UpdatedAt;
        }
    }
}