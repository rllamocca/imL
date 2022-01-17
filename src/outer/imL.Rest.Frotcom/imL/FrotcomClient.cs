using System.Net.Http;

using imL.Rest.Frotcom.Schema;

namespace imL.Rest.Frotcom
{
    public class FrotcomClient
    {
        public HttpClient Http { get; }
        public FrotcomFormat Format { get; }
        public Authorize Token { set; get; }

        public FrotcomClient(HttpClient _http, FrotcomFormat _format)
        {
            this.Http = _http;
            this.Format = _format;
        }
    }
}
