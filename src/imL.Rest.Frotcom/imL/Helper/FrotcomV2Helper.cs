using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using imL.Rest.Frotcom.Schema;

using Newtonsoft.Json;

namespace imL.Rest.Frotcom
{
    public static class FrotcomV2Helper
    {
        public async static Task<Authorize> RetriveToken(FrotcomClient _client, FormatFrotcom _auth, string _path)
        {
            if (File.Exists(_path) == false)
                File.WriteAllText(_path, "{}");

            Authorize _token = JsonConvert.DeserializeObject<Authorize>(File.ReadAllText(_path));

            if (_token == null || string.IsNullOrWhiteSpace(_token.token))
            {
                _token = await FrotcomV2Helper.AuthorizeUser(_client, _auth.Authorize);
                File.WriteAllText(_path, JsonConvert.SerializeObject(_token));

                if (_token == null || string.IsNullOrWhiteSpace(_token.token))
                    throw new Exception("RetriveToken null");

                return _token;
            }
            else
            {
                Authorize _validar = await FrotcomV2Helper.ValidateToken(_client, _token);
                if (_token.token == _validar.token)
                    return _validar;
                else
                {
                    _token = await FrotcomV2Helper.AuthorizeUser(_client, _auth.Authorize);
                    File.WriteAllText(_path, JsonConvert.SerializeObject(_token));

                    if (_token == null || string.IsNullOrWhiteSpace(_token.token))
                        throw new Exception("RetriveToken null");

                    return _token;
                }
            }
        }

        public async static Task<Authorize> AuthorizeUser(FrotcomClient _client, FormatAuthorize _auth)
        {
            StringContent _content = new StringContent(JsonConvert.SerializeObject(_auth), Encoding.UTF8, "application/json");

            using (HttpResponseMessage _res = await _client.Http.PostAsync(_client.URI + "/v2/authorize", _content))
            {
                _res.EnsureSuccessStatusCode();
                string _body = await _res.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(_body))
                    throw new Exception("ReadAsStringAsync null");

                Authorize _return = JsonConvert.DeserializeObject<Authorize>(_body);

                return _return;
            }
        }

        public async static Task<Authorize> ValidateToken(FrotcomClient _client, Authorize _auth)
        {
            string _token = "{ \"token\": \"" + _auth.token + "\" }";

            StringContent _content = new StringContent(_token, Encoding.UTF8, "application/json");

            using (HttpResponseMessage _res = await _client.Http.PutAsync(_client.URI + "/v2/authorize", _content))
            {
                _res.EnsureSuccessStatusCode();
                string _body = await _res.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(_body))
                    throw new Exception("ReadAsStringAsync null");

                Authorize _return = JsonConvert.DeserializeObject<Authorize>(_body);
                return _return;
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
                    throw new Exception("ReadAsStringAsync null");

                Vehicle[] _return = JsonConvert.DeserializeObject<Vehicle[]>(_body);

                return _return;
            }
        }
        public async static Task<Location[]> GetVehicleLocations(FrotcomClient _client, Vehicle _vehicle)
        {
            DateTime _now = DateTime.Now;

            string _uri = _client.URI + "/v2/vehicles/{1}/locations?api_key={0}&df={2}%3a{3}&allPositions=true&loadLastPosition=true";
            _uri = string.Format(_uri, _client.Authorize.token, _vehicle.id, _now.ToUniversalTime().ToString("HH"), _now.ToUniversalTime().ToString("mm"));

            using (HttpResponseMessage _res = await _client.Http.GetAsync(_uri))
            {
                _res.EnsureSuccessStatusCode();
                string _body = await _res.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(_body))
                    throw new Exception("ReadAsStringAsync null");

                Location[] _return = JsonConvert.DeserializeObject<Location[]>(_body);

                return _return;
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
                    throw new Exception("ReadAsStringAsync null");

                Account _return = JsonConvert.DeserializeObject<Account>(_body);

                return _return;
            }
        }
    }
}
