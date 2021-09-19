namespace imL.Rest.Frotcom.Schema
{
    public class Authorize
    {
        public string token { set; get; }
        public string provider { set; get; }
        public string username { set; get; }
        public Parent parent { set; get; }
        public Announcement[] announcements { set; get; }
        public bool removedSessions { set; get; }
        public int companyId { set; get; }
    }
}
