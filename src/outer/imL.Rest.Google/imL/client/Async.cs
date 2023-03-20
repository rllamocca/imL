using System;
using System.Threading;
using System.Threading.Tasks;

using imL.Rest.Google.Schema.Maps;
using imL.Utility.Http;

namespace imL.Rest.Google
{
    public partial class GoogleMapsClient
    {
        public async Task<Geocoding200> GetGeocodeAsync(decimal _lat, decimal _lng, CancellationToken _ct = default)
        {
            string _uri = "api/geocode/json?key={0}&latlng={1},{2}";
            _uri = string.Format(_uri,
                Format.Key_maps,
                Convert.ToString(_lat, ReadOnly._CULTURE_INVARIANT),
                Convert.ToString(_lng, ReadOnly._CULTURE_INVARIANT)
                );

            return await _CLIENT.GetJsonAsync<Geocoding200>(_uri, true, _ct);
        }
    }
}
