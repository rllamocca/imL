using imL.Http;

namespace imL
{
    public class EndpointFormat
    {
        public string Endpoint { set; get; }
        public EAuthentication? Scheme { set; get; }
        public string Key { set; get; }
        public string Value { set; get; }
    }
}
