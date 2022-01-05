using System.Net.Http;

namespace imL.Rest.Sbif
{
    public class SbifClient
    {
        public string URI { get; }
        public HttpClient Http { get; }
        public string ApiKey { get; }

        public SbifClient(string _uri, HttpClient _http, string _apikey)
        {
            this.URI = _uri;
            this.Http = _http;
            this.ApiKey = _apikey;
        }
    }
}
