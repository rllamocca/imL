using System.Net.Http;

namespace imL.Rest.Google
{
    public class GoogleClient
    {
        public HttpClient Http { get; }
        public GoogleFormat Format { get; }

        public GoogleClient(HttpClient _http, GoogleFormat _format)
        {
            this.Http = _http;
            this.Format = _format;

            this.Format.URI_maps = this.Format.URI_maps ?? this.Format.URI;
            this.Format.Key_maps = this.Format.Key_maps ?? this.Format.Key;
        }
    }
}
