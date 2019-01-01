using System;
using hcm.Database.Models;

namespace hcm.DTOs.Users
{
    public class UserDetailsDTO
    {
        public long UserId { get; set; }
        public string Email { get; set; }
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

        public UserDetailsDTO(User u)
        {
            UserId = u.UserId;
            Email = u.Email;
            FirstName = u.FirstName;
            LastName = u.LastName;
            Title = u.Title;
            Phone = u.Phone;
            Picture = u.Picture;
            IsPictureApproved = u.IsPictureApproved;
            IsAdmin = u.IsAdmin;
            CardId = u.CardId;
            CreatedAt = u.CreatedAt;
            UpdatedAt = u.UpdatedAt;
        }

    }
}