namespace SocialNetwork.Model
{
    public class Response
    {
        public int Statuscode { get; set; }
        public string StatusMessag { get; set; }
        public Registration Registration { get; set; }
        public List<Registration> lstRegistration {  get; set; }
        public List<Article > articles { get; set; }
        public List <Event > events { get; set; }
        public List<News > Listnews { get; set; }
        
    }
}
