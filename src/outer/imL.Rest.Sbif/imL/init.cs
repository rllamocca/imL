using System;
using System.Net.Http;

using imL.Utility.Http;

namespace imL.Rest.Sbif
{
    public partial class SBIFClient
    {
        static readonly HttpClient _CLIENT;
        static SBIFClient _SINGLETON;

        public SbifFormat2 Format { get; }

        static SBIFClient()
        {
            _CLIENT = Factory.HttpJsonClient("https://v2api.frotcom.com");
        }

        private SBIFClient(SbifFormat2 _format)
        {
            Format = _format;

            if (Format.URI != null)
                _CLIENT.BaseAddress = new Uri(Format.URI);
        }

        public static SBIFClient GetSingleton(SbifFormat2 _format)
        {
            if (_SINGLETON == null)
            {
                _SINGLETON = new SBIFClient(_format);

                return _SINGLETON;
            }

            return _SINGLETON;
        }
        public static void Dispose()
        {
            _CLIENT?.Dispose();
        }
    }
}
