namespace hcm.Controllers.Groups
{
    public class MemberDetailsResourceModel
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public bool IsGroupAdmin { get; set; }
        public string Base64Picture { get; set; }
    }
}