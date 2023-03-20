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
        public async Task<Authorize200> AuthorizeUserAsync(CancellationToken _ct = default)
        {
            return await _CLIENT.PostJsonAsync<Authorize200, Authorize>(_PATH + "authorize", Format.User, true, _ct);
        }

        public async Task<Authorize200> ValidateTokenAsync(string _token, bool _throw = false)
        {
            try
            {
                _token = "{ \"token\": \"" + _token + "\" }";

                return await _CLIENT.PutJsonAsync<Authorize200, string>(_PATH + "authorize", _token);
            }
            catch (Exception)
            {
                if (_throw)
                    throw;
            }

            return null;
        }

        public async Task<IEnumerable<Vehicle200>> GetVehiclesAsync()
        {
            string _uri = _PATH + "vehicles?api_key={0}";
            _uri = string.Format(_uri, Authorize.token);

            return await _CLIENT.GetJsonAsync<IEnumerable<Vehicle200>>(_uri);
        }
        public async Task<IEnumerable<Location200>> GetVehicleLocationsAsync(long _vehicle_id)
        {
            DateTime _now = DateTime.Now.ToUniversalTime();
            string _uri = _PATH + "vehicles/{1}/locations?api_key={0}&df={2}%3a{3}&allPositions=true&loadLastPosition=true";
            _uri = string.Format(_uri, Authorize.token, _vehicle_id, _now.ToString("HH"), _now.ToString("mm"));

            return await _CLIENT.GetJsonAsync<Location200[]>(_uri);
        }
        public async Task<Location200> GetVehicleLocationAsync(long _vehicle_id)
        {
            IEnumerable<Location200> _locations = await GetVehicleLocationsAsync(_vehicle_id);

            if (_locations.HasValue())
                return _locations.FirstOrDefault();

            return null;
        }
        public async Task<Account200> GetAccountAsync()
        {
            string _uri = _PATH + "accounts?api_key={0}";
            _uri = string.Format(_uri, Authorize.token);

            return await _CLIENT.GetJsonAsync<Account200>(_uri);
        }

        public async Task<IEnumerable<Driver200>> GetDrivers()
        {
            string _uri = _PATH + "drivers?api_key={0}";
            _uri = string.Format(_uri, Authorize.token);

            return await _CLIENT.GetJsonAsync<IEnumerable<Driver200>>(_uri);
        }
        public async Task<Driver200> GetDriver(long _driver_id)
        {
            string _uri = _PATH + "drivers/{0}?api_key={1}";
            _uri = string.Format(_uri, _driver_id, Authorize.token);

            return await _CLIENT.GetJsonAsync<Driver200>(_uri);
        }


        public async Task<IEnumerable<Dough>> ToPrepareAsync()
        {
            IEnumerable<Vehicle200> _vehicles = await GetVehiclesAsync();

            if (_vehicles.IsEmpty())
                return null;

            //if (_setting.LicensePlates.HasValue())
            //{
            //    _setting.LicensePlates.ToList().ForEach(_fe => _fe = _fe.Replace(" ", ""));
            //}

            IList<Dough> _return = new List<Dough>();

            foreach (Vehicle200 _item in _vehicles)
            {
                if (Format.LicensePlates.HasValue())
                    if (Format.LicensePlates.Any(_w => string.Equals(_w.Replace(" ", ""), _item.licensePlate.Replace(" ", ""), StringComparison.OrdinalIgnoreCase)) == false)
                        continue;

                Location200 _location = await GetVehicleLocationAsync(_item.id);

                _return.Add(new Dough(_item, _location));
            }

            return _return;
        }
    }
}