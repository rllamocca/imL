using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading;
using System.Threading.Tasks;

using imL.Rest.Frotcom.Schema;
using imL.Utility;
using imL.Utility.Http;

namespace imL.Rest.Frotcom
{
    public static class FrotcomHelperAsync
    {
        public static async Task<Authorize> AuthorizeUserAsync(FrotcomClient _client, CancellationToken _ct = default)
        {
            return await _client.Http.PostJsonAsync<Authorize, AuthorizeFormat>(_client.Format.URI + "/v2/authorize", _client.Format.Authorize, true, _ct);
            //return await _client.Http.PostJsonAsync<Authorize>(_client.Format.URI + "/v2/authorize", HttpJsonHelper.JsonContent(_client.Format.Authorize), true, _ct);
        }

        public static async Task<Authorize> ValidateTokenAsync(FrotcomClient _client, string _token, bool _throw = false)
        {
            try
            {
                _token = "{ \"token\": \"" + _token + "\" }";

                return await _client.Http.PutJsonAsync<Authorize, string>(_client.Format.URI + "/v2/authorize", _token);
            }
            catch (Exception)
            {
                if (_throw)
                    throw;
            }

            return null;
        }

        public static async Task<Vehicle[]> GetVehiclesAsync(FrotcomClient _client)
        {
            string _uri = _client.Format.URI + "/v2/vehicles?api_key={0}";
            _uri = string.Format(_uri, _client.Token.token);

            return await _client.Http.GetJsonAsync<Vehicle[]>(_uri);
        }
        public static async Task<Location[]> GetVehicleLocationsAsync(FrotcomClient _client, long _vehicle_id)
        {
            DateTime _now = DateTime.Now.ToUniversalTime();
            string _uri = _client.Format.URI + "/v2/vehicles/{1}/locations?api_key={0}&df={2}%3a{3}&allPositions=true&loadLastPosition=true";
            _uri = string.Format(_uri, _client.Token.token, _vehicle_id, _now.ToString("HH"), _now.ToString("mm"));

            return await _client.Http.GetJsonAsync<Location[]>(_uri);
        }
        public static async Task<Location> GetVehicleLocationAsync(FrotcomClient _client, long _vehicle_id)
        {
            Location[] _locations = await FrotcomHelperAsync.GetVehicleLocationsAsync(_client, _vehicle_id);

            if (_locations.HasValue())
                return _locations.FirstOrDefault();

            return null;
        }
        public static async Task<Account> GetAccountAsync(FrotcomClient _client)
        {
            string _uri = _client.Format.URI + "/v2/accounts?api_key={0}";
            _uri = string.Format(_uri, _client.Token.token);

            return await _client.Http.GetJsonAsync<Account>(_uri);
        }

        public static async Task<Dough[]> ToPrepareAsync(FrotcomClient _client)
        {
            Vehicle[] _vehicles = await FrotcomHelperAsync.GetVehiclesAsync(_client);

            if (_vehicles.IsEmpty())
                return null;

            //if (_setting.LicensePlates.HasValue())
            //{
            //    _setting.LicensePlates.ToList().ForEach(_fe => _fe = _fe.Replace(" ", ""));
            //}

            List<Dough> _return = new List<Dough>();

            for (int _i = 0; _i < _vehicles.Length; _i++)
            {
                if (_client.Format.LicensePlates.HasValue())
                    if (_client.Format.LicensePlates.Any(_w => string.Equals(_w.Replace(" ", ""), _vehicles[_i].licensePlate.Replace(" ", ""), StringComparison.OrdinalIgnoreCase)) == false)
                        continue;

                Location _location = await FrotcomHelperAsync.GetVehicleLocationAsync(_client, _vehicles[_i].id);

                _return.Add(new Dough(_vehicles[_i], _location));
            }

            return _return.ToArray();
        }
    }
}