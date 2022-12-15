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

        public static async Task<IEnumerable<Vehicle>> GetVehiclesAsync(FrotcomClient _client)
        {
            string _uri = _client.Format.URI + "/v2/vehicles?api_key={0}";
            _uri = string.Format(_uri, _client.Token.token);

            return await _client.Http.GetJsonAsync<IEnumerable<Vehicle>>(_uri);
        }
        public static async Task<IEnumerable<Location>> GetVehicleLocationsAsync(FrotcomClient _client, long _vehicle_id)
        {
            DateTime _now = DateTime.Now.ToUniversalTime();
            string _uri = _client.Format.URI + "/v2/vehicles/{1}/locations?api_key={0}&df={2}%3a{3}&allPositions=true&loadLastPosition=true";
            _uri = string.Format(_uri, _client.Token.token, _vehicle_id, _now.ToString("HH"), _now.ToString("mm"));

            return await _client.Http.GetJsonAsync<Location[]>(_uri);
        }
        public static async Task<Location> GetVehicleLocationAsync(FrotcomClient _client, long _vehicle_id)
        {
            IEnumerable<Location> _locations = await GetVehicleLocationsAsync(_client, _vehicle_id);

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

        public static async Task<IEnumerable<Driver>> GetDrivers(FrotcomClient _client)
        {
            string _format = _client.Format.URI + "/v2/drivers?api_key={0}";
            _format = string.Format(_format, _client.Token.token);

            return await _client.Http.GetJsonAsync<IEnumerable<Driver>>(_format);
        }
        public static async Task<Driver> GetDriver(FrotcomClient _client, long _driver_id)
        {
            string _format = _client.Format.URI + "/v2/drivers/{0}?api_key={1}";
            _format = string.Format(_format, _driver_id, _client.Token.token);

            return await _client.Http.GetJsonAsync<Driver>(_format);
        }


        public static async Task<IEnumerable<Dough>> ToPrepareAsync(FrotcomClient _client)
        {
            IEnumerable<Vehicle> _vehicles = await GetVehiclesAsync(_client);

            if (_vehicles.IsEmpty())
                return null;

            //if (_setting.LicensePlates.HasValue())
            //{
            //    _setting.LicensePlates.ToList().ForEach(_fe => _fe = _fe.Replace(" ", ""));
            //}

            IList<Dough> _return = new List<Dough>();

            foreach (Vehicle _item in _vehicles)
            {
                if (_client.Format.LicensePlates.HasValue())
                    if (_client.Format.LicensePlates.Any(_w => string.Equals(_w.Replace(" ", ""), _item.licensePlate.Replace(" ", ""), StringComparison.OrdinalIgnoreCase)) == false)
                        continue;

                Location _location = await GetVehicleLocationAsync(_client, _item.id);

                _return.Add(new Dough(_item, _location));
            }

            return _return;
        }
    }
}