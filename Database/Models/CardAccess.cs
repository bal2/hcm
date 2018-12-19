using System;

namespace hcm.Database.Models
{
    public class CardAccess
    {
        public long AccessId { get; set; }
        public string CardId { get; set; }
        public long UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}