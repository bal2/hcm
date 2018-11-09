using System;
using hcm.Database.Models;

namespace hcm.Controllers.Me
{
    public class MeResourceModel
    {
        public long UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public MeResourceModel(User u)
        {
            this.UserId = u.UserId;
            this.Email = u.Email;
            this.FirstName = u.FirstName;
            this.LastName = u.LastName;
            this.Title = u.Title;
            this.Phone = u.Phone;
            this.CreatedAt = u.CreatedAt;
            this.UpdatedAt = u.UpdatedAt;
        }
    }
}