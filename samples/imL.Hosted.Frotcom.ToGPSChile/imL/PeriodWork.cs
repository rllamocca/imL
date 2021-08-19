using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

using imL.Rest.Frotcom;
using imL.Rest.Frotcom.Schema;
using imL.Rest.Google;
using imL.Tool.Frotcom.ToGPSChile;

using Microsoft.Extensions.Logging;

using SOAP_Registro;

namespace imL.Hosted.Frotcom.ToGPSChile
{
    public static class PeriodWork
    {
        public async static Task DoWork(ILogger _logger)
        {
            try
            {
                Authorize _token = await FrotcomV2Helper.RetriveToken(
                    new FrotcomClient(AppLocked.Setting.Frotcom.URI, AppLocked.Http, null),
                    AppLocked.Setting.Frotcom,
                    Path.Combine(AppLocked.PathApp, "token.json")
                    );
                FrotcomClient _frotcom = new(AppLocked.Setting.Frotcom.URI, AppLocked.Http, _token);
                GoogleClient _google = new(AppLocked.Setting.Google.URI, AppLocked.Http, AppLocked.Setting.Google.Key);

                Dough[] _doughs = await FromFrotcom.Prepare(
                    AppLocked.Setting,
                    _frotcom,
                    _google,
                    _logger);

                if (_doughs == null)
                    return;

                List<actividad> _req = new();

                foreach (Dough _item in _doughs)
                {
                    if (_item.Vehicle.lastCommunication.HasValue == false)
                        continue;

                    if (AppLocked.Setting.Frotcom.LicensePlates != null)
                        if (Array.Exists(AppLocked.Setting.Frotcom.LicensePlates, _w => _w == _item.Vehicle.licensePlate) == false)
                            continue;

                    actividad _actividad = new();
                    _actividad.datosActividad = new datosActividad();
                    _actividad.datosCliente = new datosCliente();
                    datosActividad _movil = _actividad.datosActividad;
                    datosCliente _cliente = _actividad.datosCliente;
                    _cliente.flota = new flota();
                    _cliente.grupo = new grupo();
                    _cliente.operador = new operador();
                    _cliente.proveedor = new proveedor();
                    _cliente.proveedorGps = new proveedorGps();
                    _cliente.vehiculo = new vehiculo();

                    if (string.IsNullOrWhiteSpace(_item.Vehicle.typeName)) 
                        _item.Vehicle.typeName = "";
                    if (string.IsNullOrWhiteSpace(_item.Vehicle.licensePlate)) 
                        _item.Vehicle.licensePlate = "";

                    _item.Vehicle.typeName = _item.Vehicle.typeName.ToUpper().Trim();
                    _item.Vehicle.licensePlate = _item.Vehicle.licensePlate.ToUpper().Trim();

                    _item.Vehicle.licensePlate = _item.Vehicle.licensePlate.Replace(" ", "");

                    _movil.codConductor = _item.Vehicle.driverId;
                    _movil.codTipoEvento = 1;
                    _movil.datosExternos = "";
                    _movil.distanciaIncremental = 0;
                    _movil.distanciaViaje = 0;
                    _movil.fechaHoraActividad = _item.Vehicle.lastCommunication.Value.ToLocalTime().ToString("yyyy'-'MM'-'dd'T'HH:mm:ss");
                    _movil.fechaHoraRecepcion = DateTime.Now.ToLocalTime().ToString("yyyy'-'MM'-'dd'T'HH:mm:ss");
                    _movil.hdg = 118;
                    _movil.hdop = 21;
                    _movil.horometro = Convert.ToDouble(_item.Vehicle.odometerGps);
                    _movil.ignicion = "0";
                    _movil.imei = _item.Vehicle.imei;
                    _movil.latitud = Convert.ToDouble(_item.Vehicle.latitude);
                    _movil.longitud = Convert.ToDouble(_item.Vehicle.longitude);
                    _movil.nomConductor = "N/D";
                    _movil.nomTipoEvento = "Posición vehículo";
                    _movil.numSatelites = 4;
                    _movil.odometro = Convert.ToSingle(_item.Vehicle.chronometer);
                    _movil.puerto = 1;
                    _movil.t1 = 0;
                    _movil.t2 = 0;
                    _movil.t3 = 0;
                    _movil.t4 = 0;
                    _movil.tipoEvento = "";
                    _movil.velocidad = Convert.ToSingle(_item.Vehicle.speed);
                    _movil.velocidadMaxima = 100;

                    if (_item.Location != null)
                    {
                        _movil.ignicion = _item.Location.isIgnitionOn ? "1" : "0";

                        if (_item.Location.alarmOccurrenceIds != null && _item.Location.alarmOccurrenceIds.Length > 0)
                            _movil.codTipoEvento = 2;
                    }

                    _movil.ubicacion = "N/D";
                    if (_item.Result != null)
                    {
                        _movil.ubicacion = _item.Result.formatted_address;
                    }

                    _cliente.cliente = AppLocked.Setting.Endpoint.UserName;
                    _cliente.password = AppLocked.Setting.Endpoint.Password;

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

                    _cliente.vehiculo.codigo = Convert.ToInt32(_item.Vehicle.id);
                    _cliente.vehiculo.nombre = _item.Vehicle.licensePlate;
                    _cliente.vehiculo.patente = _item.Vehicle.licensePlate;

                    _req.Add(_actividad);
                }

                if (_req.Count == 0)
                {
                    _logger.LogInformation("_req.Count == 0");
                    return;
                }
                    
                _logger.LogDebug("_req:");
                _logger.LogDebug(JsonSerializer.Serialize(_req));
                //await AppLocked.Client.OpenAsync();
                try
                {
                    cargaActividadesResponse _res = await AppLocked.Soap.cargaActividadesAsync(_req.ToArray());
                    _logger.LogInformation("_res:");
                    _logger.LogInformation(JsonSerializer.Serialize(_res));
                }
                catch (Exception _ex)
                {
                    _logger.LogCritical(0, _ex, _ex.Message);
                }
                //await AppLocked.Client.CloseAsync();
            }
            catch (Exception _ex)
            {
                _logger.LogCritical(0, _ex, _ex.Message);
            }
        }
    }
}
