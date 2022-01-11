using imL.Contract;
using imL.Tool.Terminal;

using NLog;

namespace SAMPLE.imL.Tool.Terminal
{
    internal class MyProgram
    {
        public static async Task DoWork(IProcessInfo _info, ISettings _settings)
        {
            Logger _logger = LogManager.GetCurrentClassLogger();
            MySettings _sett = (MySettings)_settings;

            _logger.Info("Hi Process");

            await Task.Delay(1000);
        }
    }
}