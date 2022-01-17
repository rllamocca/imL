using System.Text.Json;

using imL.Enumeration.Logging;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace imL.Package.Hosting
{
    public static class IHostBuilderExtension
    {
        public static IHostBuilder UseSimpleLogging(this IHostBuilder _this, EConsoleFormatter _output = EConsoleFormatter.Systemd)
        {
            //_opt.TimestampFormat = "yyyy'-'MM'-'dd HH':'mm':'ss'.'ffff ";
            string _tsf = "HH':'mm':'ss'.'ffff ";

            return _this.ConfigureLogging(_lg =>
            {
                switch (_output)
                {
                    case EConsoleFormatter.Systemd:
                        _lg.AddSystemdConsole(_opt =>
                        {
                            _opt.IncludeScopes = true;
                            _opt.TimestampFormat = _tsf;
                        });

                        break;
                    case EConsoleFormatter.Simple:
                        _lg.AddSimpleConsole(_opt =>
                        {
                            _opt.SingleLine = true;
                            _opt.IncludeScopes = true;
                            _opt.TimestampFormat = _tsf;
                        });

                        break;
                    case EConsoleFormatter.Json:
                        _lg.AddJsonConsole(_opt =>
                        {
                            _opt.IncludeScopes = false;
                            _opt.TimestampFormat = _tsf;
                            _opt.JsonWriterOptions = new JsonWriterOptions
                            {
                                Indented = true
                            };
                        });

                        break;
                    default:
                        break;
                }
            });
        }
    }
}
