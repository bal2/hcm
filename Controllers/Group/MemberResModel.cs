namespace hcm.Controllers.Groups
{
    public class MemberResourceModel
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public bool IsGroupAdmin { get; set; }
    }
}