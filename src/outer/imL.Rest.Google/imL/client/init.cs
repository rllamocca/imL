using System;
using System.Net.Http;

using imL.Utility.Http;

namespace imL.Rest.Google
{
    public partial class GoogleClient
    {
        static readonly HttpClient _MAPS_CLIENT;
        static GoogleClient _SINGLETON;

        public GoogleFormat Format { get; }

        static GoogleClient()
        {
            _MAPS_CLIENT = Factory.HttpJsonClient("https://maps.googleapis.com/maps");
        }

        private GoogleClient(GoogleFormat _format)
        {
            Format = _format;

            Format.URI_maps = Format.URI_maps ?? Format.URI;
            Format.Key_maps = Format.Key_maps ?? Format.Key;

            if (Format.URI_maps != null)
                _MAPS_CLIENT.BaseAddress = new Uri(Format.URI_maps);
        }

        public static GoogleClient GetSingleton(GoogleFormat _format)
        {
            if (_SINGLETON == null)
            {
                _SINGLETON = new GoogleClient(_format);

                return _SINGLETON;
            }

            return _SINGLETON;
        }
        public static void Dispose()
        {
            _MAPS_CLIENT?.Dispose();
        }
    }
}
