using System.Net;
using System.Net.Http;

using imL.Rest.Frotcom.Schema;
using imL.Utility.Http;

namespace imL.Rest.Frotcom
{
    public partial class FrotcomClient
    {
        static readonly HttpClient _CLIENT;

        public FrotcomFormat Format { get; }
        public Authorize Token { set; get; }

        static FrotcomClient()
        {
            _CLIENT = new HttpClient(new HttpJsonHandler(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }));
        }

        public FrotcomClient(FrotcomFormat _format)
        {
            Format = _format;

            //_CLIENT.BaseAddress = new Uri(Format.URI);
        }
    }
}
