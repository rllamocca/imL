using imL.Enumeration.Http;

namespace imL.Utility.Http
{
    public class EndpointFormat
    {
        public string Endpoint { set; get; }
        public EAuthentication Scheme { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
    }
}
