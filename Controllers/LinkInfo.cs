namespace hcm.Controllers
{
    //Class to represent links in a response
    public class LinkInfo
    {
        public string Href { get; set; }
        public string Rel { get; set; }
        public string Method { get; set; }
    }
}