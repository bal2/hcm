using System;
using System.Collections.Generic;

namespace hcm.Database.Models
{
    public class User
    {
        public long UserId { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Phone { get; set; }
        public byte[] Picture { get; set; }
        public bool IsPictureApproved { get; set; }
        public bool IsAdmin { get; set; }
        public string CardId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<GroupMembership> GroupMemberships { get; set; }
        public List<RoleUser> Roles { get; set; }
    }
}