using System;
using System.Net.Http;

using imL.Utility.Http;

namespace imL.Rest.Google
{
    public partial class GoogleMapsClient
    {
        static readonly HttpClient _CLIENT;
        static GoogleMapsClient _SINGLETON;

        public GoogleFormat Format { get; }

        static GoogleMapsClient()
        {
            _CLIENT = Factory.HttpJsonClient("https://maps.googleapis.com/maps/api/");
        }

        private GoogleMapsClient(GoogleFormat _format)
        {
            Format = _format;

            Format.URI_maps = Format.URI_maps ?? Format.URI;
            Format.Key_maps = Format.Key_maps ?? Format.Key;

            if (Format.URI_maps != null)
                _CLIENT.BaseAddress = new Uri(Format.URI_maps);
        }

        public static GoogleMapsClient GetSingleton(GoogleFormat _format)
        {
            if (_SINGLETON == null)
            {
                _SINGLETON = new GoogleMapsClient(_format);

                return _SINGLETON;
            }

            return _SINGLETON;
        }
        public static Uri BaseAddress()
        {
            return _CLIENT?.BaseAddress;
        }
        public static void Dispose()
        {
            _CLIENT?.Dispose();
        }
    }
}
