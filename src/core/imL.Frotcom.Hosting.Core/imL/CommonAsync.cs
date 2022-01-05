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
    public static class CommonAsync
    {
        public static async Task<Authorize> TokenFiled(FrotcomClient _client, AuthorizeFormat _auth, bool _throw = false)
        {
            if (File.Exists(BAppLocked.App.Path) == false)
                File.WriteAllText(BAppLocked.App.Path, "{}");

#if (NETSTANDARD2_0_OR_GREATER ||  NET5_0_OR_GREATER)
            Authorize _token = JsonSerializer.Deserialize<Authorize>(File.ReadAllText(BAppLocked.App.Path));
#else
            Authorize _token = JsonConvert.DeserializeObject<Authorize>(File.ReadAllText(BLocked.App.Path));
#endif

            if (_token == null || string.IsNullOrWhiteSpace(_token.token))
            {
                _token = await FrotcomHelperAsync.AuthorizeUser(_client, _auth);

                if (_token == null || string.IsNullOrWhiteSpace(_token.token))
                    throw new Exception("AuthorizeUser null");

#if (NETSTANDARD2_0_OR_GREATER ||  NET5_0_OR_GREATER)
                File.WriteAllText(BAppLocked.App.Path, JsonSerializer.Serialize(_token));
#else
                File.WriteAllText(BLocked.App.Path, JsonConvert.SerializeObject(_token));
#endif

                return _token;
            }
            else
            {
                Authorize _validate = null;

                try
                {
                    _validate = await FrotcomHelperAsync.ValidateToken(_client, _token.token);
                }
                catch (Exception)
                {
                    if (_throw)
                        throw;
                }

                if (_validate == null || _token.token != _validate.token)
                {
                    _token = await FrotcomHelperAsync.AuthorizeUser(_client, _auth);

                    if (_token == null || string.IsNullOrWhiteSpace(_token.token))
                        throw new Exception("AuthorizeUser null");

#if (NETSTANDARD2_0_OR_GREATER ||  NET5_0_OR_GREATER)
                    File.WriteAllText(BAppLocked.App.Path, JsonSerializer.Serialize(_token));
#else
                    File.WriteAllText(BLocked.App.Path, JsonConvert.SerializeObject(_token));
#endif

                    return _token;
                }
                else
                    return _validate;
            }
        }
        public static async Task<Authorize> TokenCached(FrotcomClient _client, AuthorizeFormat _auth)
        {
            string _key = "_frotcom_token_";

            if (BAppLocked.Cache.TryGetValue(_key, out Authorize _return) == false)
            {
                _return = await FrotcomHelperAsync.AuthorizeUser(_client, _auth);
                BAppLocked.Cache.Set(_key, _return, TimeSpan.FromHours(4));
            }

            return _return;
        }
        public static async Task<Dough[]> Prepared(FrotcomClient _client, FrotcomFormat _setting, ILogger _logger)
        {
            Dough[] _doughs = await FrotcomHelperAsync.ToPrepare(_client, _setting);

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
