using System.Net.Http;

namespace imL.Rest.Google
{
    public class GoogleClient
    {
        public HttpClient Http { get; }
        public GoogleFormat Format { get; }

        public GoogleClient(HttpClient _http, GoogleFormat _format)
        {
            Http = _http;
            Format = _format;

            Format.URI_maps = Format.URI_maps ?? Format.URI;
            Format.Key_maps = Format.Key_maps ?? Format.Key;
        }
    }
}
