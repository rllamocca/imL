using System;
using System.Net.Http;
using System.Threading.Tasks;

using imL.Rest.Google.Schema.Maps;

using Newtonsoft.Json;

namespace imL.Rest.Google
{
    public static class GoogleHelper
    {
        public static async Task<Geocoding> Google_GetGeocoding(GoogleClient _client, decimal _lat, decimal _long)
        {
            string _uri = _client.URI + "/api/geocode/json?key={0}&latlng={1},{2}";
            _uri = string.Format(_uri,
                _client.KEY,
                Convert.ToString(_lat).Replace(',', '.'),
                Convert.ToString(_long).Replace(',', '.')
                );

            using (HttpResponseMessage _res = await _client.Http.GetAsync(_uri))
            {
                _res.EnsureSuccessStatusCode();
                string _body = await _res.Content.ReadAsStringAsync();

                if (_body == null)
                    throw new Exception("ReadAsStringAsync null");

                Geocoding _return = JsonConvert.DeserializeObject<Geocoding>(_body);

                return _return;
            }
        }
    }
}
