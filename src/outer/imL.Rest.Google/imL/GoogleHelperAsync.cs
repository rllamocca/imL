using System;
using System.Threading.Tasks;

using imL.Rest.Google.Schema.Maps;
using imL.Utility.Http;

namespace imL.Rest.Google
{
    public static class GoogleHelperAsync
    {
        public static async Task<Geocoding> GetGeocodingAsync(GoogleClient _client, decimal _lat, decimal _lng)
        {
            string _uri = _client.Format.URI_maps + "/api/geocode/json?key={0}&latlng={1},{2}";
            _uri = string.Format(_uri,
                _client.Format.Key_maps,
                Convert.ToString(_lat, ReadOnly._CULTURE_INVARIANT),
                Convert.ToString(_lng, ReadOnly._CULTURE_INVARIANT)
                );

            return await _client.Http.GetAsync<Geocoding>(_uri);
        }
    }
}
