using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

using imL.Rest.Frotcom.Schema;
using imL.Utility.Http;

namespace imL.Rest.Frotcom
{
    public partial class FrotcomClient
    {
        static readonly HttpClient _CLIENT;

        public FrotcomFormat Format { get; }
        public Authorize200 Authorize { set; get; }

        static FrotcomClient()
        {
            _CLIENT = new HttpClient(new HttpJsonHandler(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }));
            _CLIENT.BaseAddress = new Uri("https://v2api.frotcom.com");
            _CLIENT.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public FrotcomClient(FrotcomFormat _format)
        {
            Format = _format;

            if (Format.URI != null)
                _CLIENT.BaseAddress = new Uri(Format.URI);
        }
    }
}
