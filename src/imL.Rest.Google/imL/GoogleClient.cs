using System.Net.Http;

namespace imL.Rest.Google
{
    public class GoogleClient
    {
        public string URI { get; }
        public HttpClient Http { get; }
        public string KEY { get; }

        public GoogleClient(string _uri, HttpClient _http, string _key)
        {
            this.URI = _uri;
            this.Http = _http;
            this.KEY = _key;
        }
    }
}
