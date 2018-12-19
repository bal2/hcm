using System;
using System.Collections.Generic;

namespace hcm.Database.Models
{
    public class Group
    {
        public long GroupId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public string Description { get; set; }

        public List<GroupMembership> Members { get; set; }
    }
}