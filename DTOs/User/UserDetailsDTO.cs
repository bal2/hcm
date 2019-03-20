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
        public string Address { get; set; }
        public string Zip { get; set; }
        public string Town { get; set; }
        public string Phone { get; set; }
        public byte[] Picture { get; set; }
        public bool IsPictureApproved { get; set; }
        public string CardId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public UserDetailsDTO(User u)
        {
            UserId = u.UserId;
            Email = u.Email;
            FirstName = u.FirstName;
            LastName = u.LastName;
            Address = u.Address;
            Zip = u.Zip;
            Town = u.Town;
            Title = u.Title;
            Phone = u.Phone;
            Picture = u.Picture;
            IsPictureApproved = u.IsPictureApproved;
            CardId = u.CardId;
            CreatedAt = u.CreatedAt;
            UpdatedAt = u.UpdatedAt;
        }

    }
}