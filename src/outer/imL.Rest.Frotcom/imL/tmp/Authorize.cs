namespace imL.Rest.Frotcom.Schema
{
#pragma warning disable IDE1006 // Estilos de nombres
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
#pragma warning restore IDE1006 // Estilos de nombres
}
