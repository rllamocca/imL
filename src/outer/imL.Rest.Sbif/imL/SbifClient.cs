using System.Net.Http;

namespace imL.Rest.Sbif
{
    public class SbifClient
    {
        public HttpClient Http { get; }
        public SbifFormat Format { get; }

        public SbifClient(HttpClient _http, SbifFormat _format)
        {
            this.Http = _http;
            this.Format = _format;
        }
    }
}
