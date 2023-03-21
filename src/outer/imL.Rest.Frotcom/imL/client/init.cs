using System;
using System.Net.Http;

using imL.Rest.Frotcom.Schema;
using imL.Utility.Http;

namespace imL.Rest.Frotcom
{
    public partial class FrotcomClient
    {
        static readonly HttpClient _CLIENT;
        static FrotcomClient _SINGLETON;

        public FrotcomFormat Format { get; }
        public Authorize200 Authorize { set; get; }

        static FrotcomClient()
        {
            _CLIENT = Factory.HttpJsonClient("https://v2api.frotcom.com/v2/");
        }

        private FrotcomClient(FrotcomFormat _format)
        {
            Format = _format;

            if (Format.URI != null)
                _CLIENT.BaseAddress = new Uri(Format.URI);
        }

        public static FrotcomClient GetSingleton(FrotcomFormat _format)
        {
            if (_SINGLETON == null)
            {
                _SINGLETON = new FrotcomClient(_format);

                return _SINGLETON;
            }

            return _SINGLETON;
        }
        public static Uri GetBaseAddress()
        {
            return _CLIENT?.BaseAddress;
        }
        public static void Dispose()
        {
            _CLIENT?.Dispose();
        }
    }
}
