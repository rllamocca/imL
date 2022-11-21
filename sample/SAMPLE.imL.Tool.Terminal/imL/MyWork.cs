using imL.Contract;
using imL.Package.NLog;
using imL.Tool.Terminal;

using NLog;

namespace SAMPLE.imL.Tool.Terminal
{
    internal class MyWork
    {
        public static async Task DoWork(IProcessInfo _info, ISetting _settings)
        {
            ILogger _logger = LogManager.GetCurrentClassLogger();
            MySettings _sett = (MySettings)_settings;
            _logger.Info("Hi process");
            _logger.LetDebug()?.Debug("Debug");
            await Task.Delay(1000);
            //_info.PathAttachments.Add(@"C:\tmp\20211129-applog.log");
            _logger.Info("Bye process");
        }
    }
}