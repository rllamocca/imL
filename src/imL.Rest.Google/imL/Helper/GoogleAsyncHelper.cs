using System;
using System.Net.Http;
using System.Threading.Tasks;

using imL.Rest.Google.Schema.Maps;

using Newtonsoft.Json;

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
            {
                _res.EnsureSuccessStatusCode();
                string _body = await _res.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(_body))
                    return null;

                return JsonConvert.DeserializeObject<Geocoding>(_body); ;
            }
        }
    }
}
