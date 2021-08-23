using System;

namespace imL.Rest.Frotcom.V2.Schema
{
    public class Announcement
    {
        public int id { get; set; }
        public string title { get; set; }
        public string text { get; set; }
        public string imageTitle { get; set; }
        public string imageContent { get; set; }
        public string imageContentType { get; set; }
        public bool isLoginBlocked { get; set; }
        public string rule { get; set; }
        public string approval { get; set; }
        public bool isActive { get; set; }
        public bool isDiscarded { get; set; }
        public DateTime? expiration { get; set; }
    }
}
