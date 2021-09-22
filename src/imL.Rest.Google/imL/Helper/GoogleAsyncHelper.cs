using System;
using System.Net.Http;
using System.Threading.Tasks;

using imL.Rest.Google.Schema.Maps;

using imL.Utility.Http.Newtonsoft_Json;

namespace imL.Rest.Google
{
    public static class GoogleAsyncHelper
    {
        public async static Task<Geocoding> GetGeocoding(GoogleClient _client, decimal _lat, decimal _lng)
        {
            string _uri = _client.URI + "/api/geocode/json?key={0}&latlng={1},{2}";
            _uri = string.Format(_uri,
                _client.KEY,
                Convert.ToString(_lat, ReadOnly._CULTURE),
                Convert.ToString(_lng, ReadOnly._CULTURE)
                );

            using (HttpResponseMessage _res = await _client.Http.GetAsync(_uri))
                return await _res.ReadAsJsonAsync<Geocoding>();
        }
    }
}
