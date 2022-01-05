using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using imL.Rest.Frotcom.Schema;
using imL.Utility;
using imL.Utility.Http;

namespace imL.Rest.Frotcom
{
    public static class FrotcomHelperAsync
    {
        public static async Task<Authorize> AuthorizeUser(FrotcomClient _client, AuthorizeFormat _auth)
        {
            return await _client.Http.PostAsync<Authorize>(_client.URI + "/v2/authorize", HttpJsonHelper.JsonContent(_auth));
        }

        public static async Task<Authorize> ValidateToken(FrotcomClient _client, string _token)
        {
            _token = "{ \"token\": \"" + _token + "\" }";

            return await _client.Http.PutAsync<Authorize>(_client.URI + "/v2/authorize", HttpJsonHelper.JsonContent(_token));
        }

        public static async Task<Vehicle[]> GetVehicles(FrotcomClient _client)
        {
            string _uri = _client.URI + "/v2/vehicles?api_key={0}";
            _uri = string.Format(_uri, _client.Authorize.token);

            return await _client.Http.GetAsync<Vehicle[]>(_uri);
        }
        public static async Task<Location[]> GetVehicleLocations(FrotcomClient _client, long _vehicle_id)
        {
            DateTime _now = DateTime.Now.ToUniversalTime();
            string _uri = _client.URI + "/v2/vehicles/{1}/locations?api_key={0}&df={2}%3a{3}&allPositions=true&loadLastPosition=true";
            _uri = string.Format(_uri, _client.Authorize.token, _vehicle_id, _now.ToString("HH"), _now.ToString("mm"));

            return await _client.Http.GetAsync<Location[]>(_uri);
        }
        public static async Task<Location> GetVehicleLocation(FrotcomClient _client, long _vehicle_id)
        {
            Location[] _locations = await FrotcomHelperAsync.GetVehicleLocations(_client, _vehicle_id);

            if (_locations.HasValue())
                return _locations.FirstOrDefault();

            return null;
        }
        public static async Task<Account> GetAccount(FrotcomClient _client)
        {
            string _uri = _client.URI + "/v2/accounts?api_key={0}";
            _uri = string.Format(_uri, _client.Authorize.token);

            return await _client.Http.GetAsync<Account>(_uri);
        }

        public static async Task<Dough[]> ToPrepare(FrotcomClient _client, FrotcomFormat _setting)
        {
            Vehicle[] _vehicles = await FrotcomHelperAsync.GetVehicles(_client);

            if (_vehicles.IsEmpty())
                return null;

            //if (_setting.LicensePlates.HasValue())
            //{
            //    _setting.LicensePlates.ToList().ForEach(_fe => _fe = _fe.Replace(" ", ""));
            //}

            List<Dough> _return = new List<Dough>();

            for (int _i = 0; _i < _vehicles.Length; _i++)
            {
                if (_setting.LicensePlates.HasValue())
                    if (_setting.LicensePlates.Any(_w => string.Equals(_w.Replace(" ", ""), _vehicles[_i].licensePlate.Replace(" ", ""), StringComparison.OrdinalIgnoreCase)) == false)
                        continue;

                Location _location = await FrotcomHelperAsync.GetVehicleLocation(_client, _vehicles[_i].id);

                _return.Add(new Dough(_vehicles[_i], _location));
            }

            return _return.ToArray();
        }
    }
}