using System;
using hcm.Database.Models;

namespace hcm.DTOs.Groups
{
    public class GroupDTO
    {
        public long GroupId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public string Description { get; set; }

        //TODO: Consider adding number of members?

        public GroupDTO(Group g)
        {
            GroupId = g.GroupId;
            CreatedAt = g.CreatedAt;
            UpdatedAt = g.UpdatedAt;
            Name = g.Name;
            ShortName = g.ShortName;
            Description = g.Description;
        }
    }
}