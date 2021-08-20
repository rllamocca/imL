using System;
using System.Threading.Tasks;

using imL.Contract.Hosting;
using imL.Utility.Hosting;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using NLog.Extensions.Logging;

namespace TEST.imL.Utility.Hosting
{
    class Program
    {
        async static Task Main(string[] _args)
        {
            //ConsoleHelper.Starts();

            AppLocked.Init(_args);
            await CreateHostBuilder(_args).RunConsoleAsync();

//#if DEBUG
//            ConsoleHelper.PressAnyKeyToExit();
//#else
//            ConsoleHelper.Ends();
//#endif
        }

        static IHostBuilder CreateHostBuilder(string[] _args)
        {
            return Host.CreateDefaultBuilder(_args)
                .UseConsoleLifetime()
                .ConfigureLogging(_lg =>
                {
                    _lg.ClearProviders();
#if DEBUG
                    _lg.SetMinimumLevel(LogLevel.Trace);
#else
                    _lg.SetMinimumLevel(LogLevel.Information);
#endif
                    _lg.AddNLog();
                })
                .ConfigureServices(_ss =>
                {
                    _ss.AddSingleton<IHostSetting, HostedSetting>(_s => AppLocked.Setting.Hosted);
                    _ss.AddScoped<IPeriodWork, TestWork>();
                    _ss.AddHostedService<PeriodHostedService>();
                });
        }
    }
}
/*  
CTRL_C_EVENT
CTRL_BREAK_EVENT

static void CancelKeyPress(object _sender, ConsoleCancelEventArgs _args)
{
    Console.WriteLine("CancelKeyPress ...");
    Console.WriteLine($" SpecialKey pressed: {_args.SpecialKey}\n");

    
CTRL_LOGOFF_EVENT
CTRL_SHUTDOWN_EVENT
     

    //Console.WriteLine($"  Cancel property: {_args.Cancel}");
    // Set the Cancel property to true to prevent the process from terminating.
    //_args.Cancel = true;
}


CTRL_CLOSE_EVENT

static void ProcessExit(object _sender, EventArgs _args)
{
    Console.WriteLine("ProcessExit ...");
}

            Console.CancelKeyPress += new ConsoleCancelEventHandler(CancelKeyPress); //+C o +Pause
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(ProcessExit);

*/

/*
var config = new ConfigurationBuilder()
.SetBasePath(System.IO.Directory.GetCurrentDirectory()) //From NuGet Package Microsoft.Extensions.Configuration.Json
.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
.Build();
 */