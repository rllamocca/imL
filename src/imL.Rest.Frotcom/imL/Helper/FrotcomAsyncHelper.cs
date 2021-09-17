using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using imL.Rest.Frotcom.Schema;

using Newtonsoft.Json;

namespace imL.Rest.Frotcom
{
    public static class FrotcomAsyncHelper
    {
        public async static Task<Authorize> RetriveToken(FrotcomClient _client, FrotcomFormat _auth, string _path)
        {
            if (File.Exists(_path) == false)
                File.WriteAllText(_path, "{}");

            Authorize _token = JsonConvert.DeserializeObject<Authorize>(File.ReadAllText(_path));

            if (_token == null || string.IsNullOrWhiteSpace(_token.token))
            {
                _token = await FrotcomAsyncHelper.AuthorizeUser(_client, _auth.Authorize);
                File.WriteAllText(_path, JsonConvert.SerializeObject(_token));

                if (_token == null || string.IsNullOrWhiteSpace(_token.token))
                    throw new Exception("RetriveToken null");

                return _token;
            }
            else
            {
                Authorize _validar = await FrotcomAsyncHelper.ValidateToken(_client, _token.token);
                if (_token.token == _validar.token)
                    return _validar;
                else
                {
                    _token = await FrotcomAsyncHelper.AuthorizeUser(_client, _auth.Authorize);
                    File.WriteAllText(_path, JsonConvert.SerializeObject(_token));

                    if (_token == null || string.IsNullOrWhiteSpace(_token.token))
                        throw new Exception("RetriveToken null");

                    return _token;
                }
            }
        }

        public async static Task<Authorize> AuthorizeUser(FrotcomClient _client, AuthorizeFormat _auth)
        {
            StringContent _content = new StringContent(JsonConvert.SerializeObject(_auth), Encoding.UTF8, "application/json");

            using (HttpResponseMessage _res = await _client.Http.PostAsync(_client.URI + "/v2/authorize", _content))
            {
                _res.EnsureSuccessStatusCode();
                string _body = await _res.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(_body))
                    return null;

                return JsonConvert.DeserializeObject<Authorize>(_body);
            }
        }

        public async static Task<Authorize> ValidateToken(FrotcomClient _client, string _token)
        {
            _token = "{ \"token\": \"" + _token + "\" }";

            StringContent _content = new StringContent(_token, Encoding.UTF8, "application/json");

            using (HttpResponseMessage _res = await _client.Http.PutAsync(_client.URI + "/v2/authorize", _content))
            {
                _res.EnsureSuccessStatusCode();
                string _body = await _res.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(_body))
                    return null;

                return JsonConvert.DeserializeObject<Authorize>(_body);
            }
        }
        public async static Task<Vehicle[]> GetVehicles(FrotcomClient _client)
        {
            string _uri = _client.URI + "/v2/vehicles?api_key={0}";
            _uri = string.Format(_uri, _client.Authorize.token);

            using (HttpResponseMessage _res = await _client.Http.GetAsync(_uri))
            {
                _res.EnsureSuccessStatusCode();
                string _body = await _res.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(_body))
                    return null;

                return JsonConvert.DeserializeObject<Vehicle[]>(_body);
            }
        }

        public async static Task<Location[]> GetVehicleLocations(FrotcomClient _client, long _vehicle_id)
        {
            DateTime _now = DateTime.Now;
            string _uri = _client.URI + "/v2/vehicles/{1}/locations?api_key={0}&df={2}%3a{3}&allPositions=true&loadLastPosition=true";
            _uri = string.Format(_uri, _client.Authorize.token, _vehicle_id, _now.ToUniversalTime().ToString("HH"), _now.ToUniversalTime().ToString("mm"));

            using (HttpResponseMessage _res = await _client.Http.GetAsync(_uri))
            {
                _res.EnsureSuccessStatusCode();
                string _body = await _res.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(_body))
                    return null;

                return JsonConvert.DeserializeObject<Location[]>(_body);
            }
        }
        public async static Task<Account> GetAccount(FrotcomClient _client)
        {
            string _uri = _client.URI + "/v2/accounts?api_key={0}";
            _uri = string.Format(_uri, _client.Authorize.token);

            using (HttpResponseMessage _res = await _client.Http.GetAsync(_uri))
            {
                _res.EnsureSuccessStatusCode();
                string _body = await _res.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(_body))
                    return null;

                return JsonConvert.DeserializeObject<Account>(_body);
            }
        }

        public async static Task<Dough[]> Prepare(FrotcomFormat _setting, FrotcomClient _frotcom)
        {
            Vehicle[] _vehicles = await FrotcomAsyncHelper.GetVehicles(_frotcom);

            if (_vehicles == null || _vehicles.Length == 0)
                return null;

            List<Dough> _return = new List<Dough>();

            foreach (Vehicle _item in _vehicles)
            {
                if (_setting.LicensePlates != null)
                    if (Array.Exists(_setting.LicensePlates, _w => _w == _item.licensePlate) == false)
                        continue;

                Location[] _locations = await FrotcomAsyncHelper.GetVehicleLocations(_frotcom, _item.id);
                Location _location = null;
                if (_locations != null)
                    _location = _locations.FirstOrDefault();

                _return.Add(new Dough(_item, _location));
            }

            return _return.ToArray();
        }
    }
}
