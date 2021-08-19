using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

using imL.Rest.Frotcom;
using imL.Rest.Frotcom.imL;
using imL.Rest.Frotcom.Schema;
using imL.Rest.Google;

using Microsoft.Extensions.Logging;

using SOAP_Registro;

namespace imL.Hosted.Frotcom.ToGPSChile
{
    public class GPSChile
    {
        public async static Task ExternalWork(ILogger _logger)
        {
            try
            {
                Authorize _token = await FrotcomV2Helper.RetriveToken(new FrotcomClient(AppLocked.Setting.Frotcom.URI, AppLocked.Http, null), AppLocked.Setting.Frotcom, Path.Combine(AppLocked.PathApp, "token.json"));
                if (_token == null || string.IsNullOrWhiteSpace(_token.token))
                    throw new Exception("GetToken null");

                FrotcomClient _frotcom = new(AppLocked.Setting.Frotcom.URI, AppLocked.Http, _token);
                GoogleClient _google = new(AppLocked.Setting.Google.URI, AppLocked.Http, AppLocked.Setting.Google.Key);

                Vehicle[] _vehicles = await FrotcomV2Helper.GetVehicles(_frotcom);
                if (_vehicles == null || _vehicles.Length == 0)
                {
                    throw new Exception("GetVehicles null or 0");
                }

                string[] _licenseplates = _vehicles.Select(_s => _s.licensePlate).ToArray();

                _logger.LogDebug("_licenseplates:");
                _logger.LogDebug(JsonSerializer.Serialize(_licenseplates));

                List<actividad> _send = new();

                foreach (Vehicle _item in _vehicles)
                {
                    if (_item.lastCommunication.HasValue == false)
                        continue;

                    if (AppLocked.Setting.Frotcom.LicensePlates != null)
                        if (Array.Exists(AppLocked.Setting.Frotcom.LicensePlates, _w => _w == _item.licensePlate) == false)
                            continue;

                    actividad _actividad = new();
                    _actividad.datosActividad = new datosActividad();
                    _actividad.datosCliente = new datosCliente();
                    datosActividad _movil = _actividad.datosActividad;
                    datosCliente _cliente = _actividad.datosCliente;

                    if (String.IsNullOrWhiteSpace(_item.typeName)) _item.typeName = "";
                    if (String.IsNullOrWhiteSpace(_item.licensePlate)) _item.licensePlate = "";

                    _item.typeName = _item.typeName.ToUpper().Trim();
                    _item.licensePlate = _item.licensePlate.ToUpper().Trim();

                    _item.licensePlate = _item.licensePlate.Replace(" ", "");

                    _movil.codConductor = _item.driverId;
                    _movil.codTipoEvento = 1;
                    _movil.datosExternos = "";
                    _movil.distanciaIncremental = 0;
                    _movil.distanciaViaje = 0;
                    _movil.fechaHoraActividad = _item.lastCommunication.Value.ToLocalTime().ToString("yyyy'-'MM'-'dd'T'HH:mm:ss");
                    _movil.fechaHoraRecepcion = DateTime.Now.ToLocalTime().ToString("yyyy'-'MM'-'dd'T'HH:mm:ss");
                    _movil.hdg = 118;
                    _movil.hdop = 21;
                    _movil.horometro = Convert.ToDouble(_item.odometerGps);
                    _movil.ignicion = "0";
                    _movil.imei = _item.imei;
                    _movil.latitud = Convert.ToDouble(_item.latitude);
                    _movil.longitud = Convert.ToDouble(_item.longitude);
                    _movil.nomConductor = "N/D";
                    _movil.nomTipoEvento = "Posición vehículo";
                    _movil.numSatelites = 4;
                    _movil.odometro = Convert.ToSingle(_item.chronometer);
                    _movil.puerto = 1;
                    _movil.t1 = 0;
                    _movil.t2 = 0;
                    _movil.t3 = 0;
                    _movil.t4 = 0;
                    _movil.tipoEvento = "";

                    _movil.ubicacion = "N/D";
                    try
                    {
                        Rest.Google.Schema.Maps.Geocoding _gc = await GoogleHelper.Google_GetGeocoding(_google, _item.latitude, _item.longitude);
                        if (_gc != null)
                            _movil.ubicacion = _gc.results.FirstOrDefault().formatted_address;
                    }
                    catch (Exception _ex)
                    {
                        AppLocked.Logger.Fatal(_ex);
                    }

                    _movil.velocidad = Convert.ToSingle(_item.speed);
                    _movil.velocidadMaxima = 100;

                    _cliente.cliente = AppLocked.Setting.Endpoint.UserName;
                    _cliente.password = AppLocked.Setting.Endpoint.Password;

                    _cliente.flota = new flota();
                    _cliente.grupo = new grupo();
                    _cliente.operador = new operador();
                    _cliente.proveedor = new proveedor();
                    _cliente.proveedorGps = new proveedorGps();
                    _cliente.vehiculo = new vehiculo();

                    _cliente.flota.codigo = 1;
                    _cliente.flota.nombre = "FLOTA";

                    _cliente.grupo.codigo = 1;
                    _cliente.grupo.nombre = "GRUPO";

                    _cliente.operador.nombre = "OPERADOR";
                    _cliente.operador.rut = "1";
                    _cliente.operador.digito = "9";

                    _cliente.proveedor.nombre = AppLocked.Setting.Supplier;
                    _cliente.proveedor.rut = AppLocked.Setting.SupplierRUT;
                    _cliente.proveedor.digito = AppLocked.Setting.SupplierDV;

                    _cliente.proveedorGps.nombre = AppLocked.Setting.GPSProvider;
                    _cliente.proveedorGps.rut = AppLocked.Setting.GPSProviderRUT;
                    _cliente.proveedorGps.digito = AppLocked.Setting.GPSProviderDV;

                    _cliente.vehiculo.codigo = Convert.ToInt32(_item.id);
                    _cliente.vehiculo.nombre = _item.licensePlate;
                    _cliente.vehiculo.patente = _item.licensePlate;

                    Location[] _locations = await FrotcomV2Helper.GetVehicleLocations(_frotcom, _item);
                    Location _location = null;
                    if (_locations != null)
                        _location = _locations.FirstOrDefault();

                    if (_location != null)
                    {
                        _movil.ignicion = _location.isIgnitionOn ? "1" : "0";

                        if (_location.alarmOccurrenceIds != null && _location.alarmOccurrenceIds.Length > 0)
                            _movil.codTipoEvento = 2;
                    }

                    _send.Add(_actividad);
                }

                if (_send.Count == 0)
                    throw new Exception("_send.Count == 0");

                _logger.LogDebug("_send:");
                _logger.LogDebug(JsonSerializer.Serialize(_send));
                //await AppLocked.Client.OpenAsync();
                try
                {
                    cargaActividadesResponse _resp = await AppLocked.Soap.cargaActividadesAsync(_send.ToArray());
                    _logger.LogInformation("_resp:");
                    _logger.LogInformation(JsonSerializer.Serialize(_resp));
                }
                catch (Exception _ex)
                {
                    AppLocked.Logger.Fatal(_ex);
                }
                //await AppLocked.Client.CloseAsync();
            }
            catch (Exception _ex)
            {
                AppLocked.Logger.Fatal(_ex);
            }
        }
    }
}
