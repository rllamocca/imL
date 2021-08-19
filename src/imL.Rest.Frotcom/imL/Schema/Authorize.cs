namespace imL.Rest.Frotcom.Schema
{
    public class Authorize
    {
        public string token { get; set; }
        public string provider { get; set; }
        public string username { get; set; }
        public Parent parent { get; set; }
        public Announcement[] announcements { get; set; }
        public bool removedSessions { get; set; }
        public int companyId { get; set; }
    }
}
