using System;

namespace hcm.Database.Models
{
    public class Message
    {
        public long MessageId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public long SenderUserId { get; set; }

        //A message can be sent to a user or a group
        public long RecipientUserId { get; set; }
        public long RecipientGroupId { get; set; }

        //A message might be part of a longer message thread
        public long ParentMessageId { get; set; }

        public bool sendAsSms { get; set; }
        public bool sendAsEmail { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}