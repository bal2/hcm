using System;
using System.Collections.Generic;

namespace hcm.Database.Models
{
    public class RoleUser
    {
        public long UserId { get; set; }
        public User User { get; set; }
        public long RoleId { get; set; }
        public Role Role { get; set; }
    }
}