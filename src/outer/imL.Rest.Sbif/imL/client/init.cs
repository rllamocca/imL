using System;
using System.Globalization;
using System.Net.Http;

using imL.Utility.Http;

namespace imL.Rest.Sbif
{
    public partial class SBIFClient
    {

        static readonly HttpClient _CLIENT;
        static readonly CultureInfo _CULTURE = CultureInfo.GetCultureInfo("es-cl");
        static readonly string _ISO_4217 = (new RegionInfo(_CULTURE.LCID)).ISOCurrencySymbol;
        static readonly string _PATH = "api-sbifv3/recursos_api/";
        static SBIFClient _SINGLETON;

        public SBIFFormat Format { get; }

        static SBIFClient()
        {
            _CLIENT = Factory.HttpJsonClient("https://api.sbif.cl");
        }

        private SBIFClient(SBIFFormat _format)
        {
            Format = _format;

            if (Format.URI != null)
                _CLIENT.BaseAddress = new Uri(Format.URI);
        }

        public static SBIFClient GetSingleton(SBIFFormat _format)
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
