using System;
using System.Net.Http;

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
            _CLIENT = Factory.HttpJsonClient("https://v2api.frotcom.com");
        }

        public FrotcomClient(FrotcomFormat _format)
        {
            Format = _format;

            if (Format.URI != null)
                _CLIENT.BaseAddress = new Uri(Format.URI);
        }
    }
}
