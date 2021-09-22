using System;

namespace imL.Rest.Frotcom.Schema
{
    public class Announcement
    {
        public int id { set; get; }
        public string title { set; get; }
        public string text { set; get; }
        public string imageTitle { set; get; }
        public string imageContent { set; get; }
        public string imageContentType { set; get; }
        public bool isLoginBlocked { set; get; }
        public string rule { set; get; }
        public string approval { set; get; }
        public bool isActive { set; get; }
        public bool isDiscarded { set; get; }
        public DateTime? expiration { set; get; }
    }
}
