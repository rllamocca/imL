﻿#if (NETSTANDARD2_0_OR_GREATER ||  NET5_0_OR_GREATER)
#else
using Newtonsoft.Json;
#endif

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using imL.Package.Logging;
using imL.Rest.Frotcom;
using imL.Rest.Frotcom.Schema;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace imL.Frotcom.Hosting.Core
{
    public static class CoreHelperAsync
    {
        static readonly object _LOCK = new object();
        static readonly MemoryCache _CACHE = new MemoryCache(new MemoryCacheOptions());

//        public static async Task<Authorize200> FillTokenFiledAsync(FrotcomClient _client, bool _throw = false)
//        {
//            string _file = Path.Combine(LockedHost.App.Base, "_frotcom_token_.json");

//            if (File.Exists(_file) == false)
//                File.WriteAllText(_file, "{}");

//#if (NETSTANDARD2_0_OR_GREATER ||  NET5_0_OR_GREATER)
//            Authorize200 _token = JsonSerializer.Deserialize<Authorize200>(File.ReadAllText(_file));
//#else
//            Authorize200 _token = JsonConvert.DeserializeObject<Authorize200>(File.ReadAllText(_file));
//#endif

//            if (_token == null || string.IsNullOrWhiteSpace(_token.token))
//            {
//                _token = await _client.AuthorizeUserAsync();

//                if (_token == null || string.IsNullOrWhiteSpace(_token.token))
//                    throw new Exception("AuthorizeUser null");

//#if (NETSTANDARD2_0_OR_GREATER ||  NET5_0_OR_GREATER)
//                File.WriteAllText(_file, JsonSerializer.Serialize(_token));
//#else
//                File.WriteAllText(_file, JsonConvert.SerializeObject(_token));
//#endif

//                return _token;
//            }
//            else
//            {
//                Authorize200 _validate = await _client.ValidateTokenAsync(_token.token, _throw);

//                if (_validate == null || _token.token != _validate.token)
//                {
//                    _token = await _client.AuthorizeUserAsync();

//                    if (_token == null || string.IsNullOrWhiteSpace(_token.token))
//                        throw new Exception("AuthorizeUser null");

//#if (NETSTANDARD2_0_OR_GREATER ||  NET5_0_OR_GREATER)
//                    File.WriteAllText(_file, JsonSerializer.Serialize(_token));
//#else
//                    File.WriteAllText(_file, JsonConvert.SerializeObject(_token));
//#endif
//                    return _token;
//                }
//                else
//                    return _validate;
//            }
//        }
        public static async Task<Authorize200> FillTokenCachedAsync(FrotcomClient _client)
        {
            string _key = "_frotcom_token_";

            if (_CACHE.TryGetValue(_key, out Authorize200 _return) == false)
            {
                _return = await _client.AuthorizeUserAsync();

                if (_client.Format.LifeTime == null)
                    _client.Format.LifeTime = 4;

                _CACHE.Set(_key, _return, TimeSpan.FromHours(_client.Format.LifeTime.GetValueOrDefault()));
            }

            return _return;
        }

        public static async Task<IEnumerable<Dough>> PreparedNoStopAsync(FrotcomClient _client, ILogger _logger)
        {
            IEnumerable<Dough> _doughs = await _client.ToPrepareAsync();

            if (_doughs.IsEmpty())
            {
                _logger?.LogInformation("_doughs.Length == 0");

                return null;
            }

            _logger?.LetDebug()?.LogDebug("{p0}", string.Join("|", _doughs.Select(_s => _s.Vehicle.licensePlate).Distinct().OrderBy(_o => _o).ToArray()));
            bool _sstop = (DateTime.Now.Minute % 20 == 0);
            IList<Dough> _return = new List<Dough>();

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

            return _return;
        }
        public static async Task<IEnumerable<Dough>> PreparedAsync(FrotcomClient _client, ILogger _logger)
        {
            IEnumerable<Dough> _doughs = await _client.ToPrepareAsync();

            if (_doughs.IsEmpty())
            {
                _logger?.LogInformation("_doughs.Length == 0");

                return null;
            }

            _logger?.LetDebug()?.LogDebug("{p0}", string.Join("|", _doughs.Select(_s => _s.Vehicle.licensePlate).Distinct().OrderBy(_o => _o).ToArray()));
            IList<Dough> _return = new List<Dough>();

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
                }

                _item.Vehicle.licensePlate = _item.Vehicle.licensePlate.Trim().Replace(" ", "").ToUpper();

                _return.Add(_item);
            }

            return _return;
        }
    }
}
