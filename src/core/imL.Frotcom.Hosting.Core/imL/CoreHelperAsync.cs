#if (NETSTANDARD2_0_OR_GREATER ||  NET5_0_OR_GREATER)
using System.Text.Json;
#else
using Newtonsoft.Json;
#endif

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using imL.Rest.Frotcom;
using imL.Rest.Frotcom.Schema;
using imL.Utility.Logging;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace imL.Frotcom.Hosting.Core
{
    public static class CoreHelperAsync
    {
        public static async Task FillTokenFiledAsync(FrotcomClient _client, bool _throw = false)
        {
            string _file = Path.Combine(LockedHost.App.Path, "_frotcom_token_.json");

            if (File.Exists(_file) == false)
                File.WriteAllText(_file, "{}");

#if (NETSTANDARD2_0_OR_GREATER ||  NET5_0_OR_GREATER)
            Authorize _token = JsonSerializer.Deserialize<Authorize>(File.ReadAllText(_file));
#else
            Authorize _token = JsonConvert.DeserializeObject<Authorize>(File.ReadAllText(_path));
#endif

            if (_token == null || string.IsNullOrWhiteSpace(_token.token))
            {
                _token = await FrotcomHelperAsync.AuthorizeUserAsync(_client);

                if (_token == null || string.IsNullOrWhiteSpace(_token.token))
                    throw new Exception("AuthorizeUser null");

#if (NETSTANDARD2_0_OR_GREATER ||  NET5_0_OR_GREATER)
                File.WriteAllText(_file, JsonSerializer.Serialize(_token));
#else
                File.WriteAllText(_file, JsonConvert.SerializeObject(_token));
#endif

                _client.Token = _token;
            }
            else
            {
                Authorize _validate = await FrotcomHelperAsync.ValidateTokenAsync(_client, _token.token, _throw);

                if (_validate == null || _token.token != _validate.token)
                {
                    _token = await FrotcomHelperAsync.AuthorizeUserAsync(_client);

                    if (_token == null || string.IsNullOrWhiteSpace(_token.token))
                        throw new Exception("AuthorizeUser null");

#if (NETSTANDARD2_0_OR_GREATER ||  NET5_0_OR_GREATER)
                    File.WriteAllText(_file, JsonSerializer.Serialize(_token));
#else
                    File.WriteAllText(_file, JsonConvert.SerializeObject(_token));
#endif
                    _client.Token = _token;
                }
                else
                    _client.Token = _validate;
            }
        }
        public static async Task FillTokenCachedAsync(FrotcomClient _client)
        {
            string _key = "_frotcom_token_";

            if (LockedHost.Cache.TryGetValue(_key, out Authorize _return) == false)
            {
                _return = await FrotcomHelperAsync.AuthorizeUserAsync(_client);
                LockedHost.Cache.Set(_key, _return, TimeSpan.FromHours(4));
            }

            _client.Token = _return;
        }
        public static async Task<Dough[]> PreparedAsync(FrotcomClient _client, ILogger _logger)
        {
            Dough[] _doughs = await FrotcomHelperAsync.ToPrepareAsync(_client);

            if (_doughs == null || _doughs.Length == 0)
            {
                _logger?.LogInformation("_doughs.Length == 0");

                return null;
            }

            _logger?.LetDebug()?.LogDebug("{p0}", string.Join("|", _doughs.Select(_s => _s.Vehicle.licensePlate).Distinct().OrderBy(_o => _o).ToArray()));
            bool _sstop = (DateTime.Now.Minute % 20 == 0);
            List<Dough> _return = new List<Dough>();

            foreach (Dough _item in _doughs)
            {
                if (string.IsNullOrWhiteSpace(_item.Vehicle.licensePlate))
                {
                    _logger?.LogInformation("licensePlate IsNullOrWhiteSpace: {p0}", _item.Vehicle.id);

                    continue;
                }

                if (_item.Vehicle.lastCommunication.HasValue == false)
                {
                    _logger?.LogInformation("lastCommunication FALSE: {p0}| {p1}| {p2}",
                        _item.Vehicle.licensePlate,
                        _item.Vehicle.latitude,
                        _item.Vehicle.longitude);

                    continue;
                }

                if (_item.Vehicle.isStopped)
                {
                    _logger?.LogInformation("isStopped: {p0}| {p1}| {p2}| {p3}| {p4}",
                        _item.Vehicle.licensePlate,
                        _item.Vehicle.latitude,
                        _item.Vehicle.longitude,
                        _item.Vehicle.lastCommunication.Value.ToLocalTime(),
                        TimeSpan.FromSeconds(_item.Vehicle.stopDuration));

                    if (_sstop == false)
                        continue;
                }

                _item.Vehicle.licensePlate = _item.Vehicle.licensePlate.Trim().Replace(" ", "").ToUpper();

                _return.Add(_item);
            }

            if (_sstop)
                _logger?.LogInformation("refer isStopped");

            return _return.ToArray();
        }
    }
}
