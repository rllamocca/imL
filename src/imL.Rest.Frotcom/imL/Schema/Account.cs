namespace imL.Rest.Frotcom.Schema
{
    public class Account
    {
        public int id { get; set; }
        public int partnerId { get; set; }
        public string partnerName { get; set; }
        public string name { get; set; }
        public string fullname { get; set; }
        public int industry { get; set; }
        public string website { get; set; }
        public bool disclaimer { get; set; }
        public string address { get; set; }
        public string vatNumber { get; set; }
    }
}
