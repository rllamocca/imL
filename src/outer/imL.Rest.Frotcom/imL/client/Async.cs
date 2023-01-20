using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using imL.Rest.Frotcom.Schema;
using imL.Utility.Http;

namespace imL.Rest.Frotcom
{
    public partial class FrotcomClient
    {
        public async Task<Authorize> AuthorizeUserAsync(CancellationToken _ct = default)
        {
            return await _CLIENT.PostJsonAsync<Authorize, AuthorizeFormat>("/authorize", Format.Authorize, true, _ct);
        }

        public async Task<Authorize> ValidateTokenAsync(string _token, bool _throw = false)
        {
            try
            {
                _token = "{ \"token\": \"" + _token + "\" }";

                return await _CLIENT.PutJsonAsync<Authorize, string>("/v2/authorize", _token);
            }
            catch (Exception)
            {
                if (_throw)
                    throw;
            }

            return null;
        }

        public async Task<IEnumerable<Vehicle>> GetVehiclesAsync()
        {
            string _uri = "/vehicles?api_key={0}";
            _uri = string.Format(_uri, Token.token);

            return await _CLIENT.GetJsonAsync<IEnumerable<Vehicle>>(_uri);
        }
        public async Task<IEnumerable<Location>> GetVehicleLocationsAsync(long _vehicle_id)
        {
            DateTime _now = DateTime.Now.ToUniversalTime();
            string _uri = "/v2/vehicles/{1}/locations?api_key={0}&df={2}%3a{3}&allPositions=true&loadLastPosition=true";
            _uri = string.Format(_uri, Token.token, _vehicle_id, _now.ToString("HH"), _now.ToString("mm"));

            return await _CLIENT.GetJsonAsync<Location[]>(_uri);
        }
        public async Task<Location> GetVehicleLocationAsync(long _vehicle_id)
        {
            IEnumerable<Location> _locations = await GetVehicleLocationsAsync(_vehicle_id);

            if (_locations.HasValue())
                return _locations.FirstOrDefault();

            return null;
        }
        public async Task<Account> GetAccountAsync()
        {
            string _uri = "/v2/accounts?api_key={0}";
            _uri = string.Format(_uri, Token.token);

            return await _CLIENT.GetJsonAsync<Account>(_uri);
        }

        public async Task<IEnumerable<Driver>> GetDrivers()
        {
            string _format = "/v2/drivers?api_key={0}";
            _format = string.Format(_format, Token.token);

            return await _CLIENT.GetJsonAsync<IEnumerable<Driver>>(_format);
        }
        public async Task<Driver> GetDriver(long _driver_id)
        {
            string _format = "/v2/drivers/{0}?api_key={1}";
            _format = string.Format(_format, _driver_id, Token.token);

            return await _CLIENT.GetJsonAsync<Driver>(_format);
        }


        public async Task<IEnumerable<Dough>> ToPrepareAsync()
        {
            IEnumerable<Vehicle> _vehicles = await GetVehiclesAsync();

            if (_vehicles.IsEmpty())
                return null;

            //if (_setting.LicensePlates.HasValue())
            //{
            //    _setting.LicensePlates.ToList().ForEach(_fe => _fe = _fe.Replace(" ", ""));
            //}

            IList<Dough> _return = new List<Dough>();

            foreach (Vehicle _item in _vehicles)
            {
                if (Format.LicensePlates.HasValue())
                    if (Format.LicensePlates.Any(_w => string.Equals(_w.Replace(" ", ""), _item.licensePlate.Replace(" ", ""), StringComparison.OrdinalIgnoreCase)) == false)
                        continue;

                Location _location = await GetVehicleLocationAsync(_item.id);

                _return.Add(new Dough(_item, _location));
            }

            return _return;
        }
    }
}