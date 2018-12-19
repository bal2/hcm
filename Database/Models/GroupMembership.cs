using System;

namespace hcm.Database.Models
{
    public class GroupMembership
    {
        public long UserId { get; set; }
        public User User { get; set; }

        public long GroupId { get; set; }
        public Group Group { get; set; }

        public bool IsGroupAdmin { get; set; }

    }
}