using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using imL.Rest.Frotcom;
using imL.Rest.Frotcom.Schema;
using imL.Rest.Google;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

namespace imL.Tool.Frotcom.ToGPSChile
{
    public static class FromFrotcom
    {
        public async static Task<Dough[]> Prepare(Settings _setting, FrotcomClient _frotcom, GoogleClient _google, ILogger _logger)
        {
            try
            {
                Vehicle[] _vehicles = await FrotcomV2Helper.GetVehicles(_frotcom);
                if (_vehicles == null || _vehicles.Length == 0)
                    return null;

                string[] _licenseplates = _vehicles.Select(_s => _s.licensePlate).ToArray();

                _logger.LogDebug("_licenseplates:");
                _logger.LogDebug(JsonConvert.SerializeObject(_licenseplates));

                List<Dough> _return = new List<Dough>();

                foreach (Vehicle _item in _vehicles)
                {
                    if (_item.lastCommunication.HasValue == false)
                        continue;

                    if (_setting.Frotcom.LicensePlates != null)
                        if (Array.Exists(_setting.Frotcom.LicensePlates, _w => _w == _item.licensePlate) == false)
                            continue;

                    Location[] _locations = await FrotcomV2Helper.GetVehicleLocations(_frotcom, _item);
                    Location _location = null;
                    if (_locations != null)
                        _location = _locations.FirstOrDefault();

                    Rest.Google.Schema.Maps.Geocoding _gc = await GoogleHelper.GetGeocoding(_google, _item.latitude, _item.longitude);
                    Rest.Google.Schema.Maps.Result[] _results = _gc.results;
                    Rest.Google.Schema.Maps.Result _result = null;
                    if (_results != null)
                        _result = _results.FirstOrDefault();

                    _return.Add(new Dough(_item, _location, _result));
                }

                return _return.ToArray();
            }
            catch (Exception _ex)
            {
                _logger.LogCritical(0, _ex, _ex.Message);
            }

            return null;
        }
    }
}
