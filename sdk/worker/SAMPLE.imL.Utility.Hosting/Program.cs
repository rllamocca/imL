using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

using imL;
using imL.Package.Hosting;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using NLog.Extensions.Logging;

namespace SAMPLE.imL.Utility.Hosting
{
    public class Program
    {
        async static Task Main()
        {
            var _args = new string[] { "Richie", "Tepes" };

            var _info = new AppInfoDefault(_args);
            var _setting = JsonSerializer.Deserialize<MySetting>(File.ReadAllText(Path.Combine(_info.Base, "settings.json")));

            var _app = HostHelper.CreatePeriodHostBuilder<MyWorker>(_info, _setting);
            //_app.UseSimpleLogging(EConsoleFormatter.Simple);
            _app.ConfigureLogging(
                _lg =>
                {
                    _lg.ClearProviders();
                    //#if DEBUG
                    //                    _lg.SetMinimumLevel(LogLevel.Trace);
                    //#else
                    //                    _lg.SetMinimumLevel(LogLevel.Information);
                    //#endif
                    _lg.AddNLog();
                });

            await _app.RunConsoleAsync();
        }
    }
}
