using System.Net.Http;

using imL.Rest.Frotcom.Schema;

namespace imL.Rest.Frotcom.imL
{
    public class FrotcomClient
    {
        public string URI { get; }
        public HttpClient Http { get; }
        public Authorize Authorize { get; }

        public FrotcomClient(string _uri, HttpClient _http, Authorize _auth)
        {
            this.URI = _uri;
            this.Http = _http;
            this.Authorize = _auth;
        }
    }
}
