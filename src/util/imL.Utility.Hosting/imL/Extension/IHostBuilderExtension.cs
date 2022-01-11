using imL.Enumeration.Logging;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System.Text.Json;

namespace imL.Utility.Hosting
{
    public static class IHostBuilderExtension
    {
        public static IHostBuilder UseSimpleLogging(this IHostBuilder _this, EConsoleOutput _output = EConsoleOutput.Systemd)
        {
            //_opt.TimestampFormat = "yyyy'-'MM'-'dd HH':'mm':'ss'.'ffff ";
            string _tsf = "HH':'mm':'ss'.'ffff ";

            return _this.ConfigureLogging(_lg =>
            {
                switch (_output)
                {
                    case EConsoleOutput.None:
                        break;
                    case EConsoleOutput.Systemd:
                        _lg.AddSystemdConsole(_opt =>
                        {
                            _opt.IncludeScopes = true;
                            _opt.TimestampFormat = _tsf;
                        });

                        break;
                    case EConsoleOutput.Simple:
                        _lg.AddSimpleConsole(_opt =>
                        {
                            _opt.SingleLine = true;
                            _opt.IncludeScopes = true;
                            _opt.TimestampFormat = _tsf;
                        });

                        break;
                    case EConsoleOutput.Json:
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
