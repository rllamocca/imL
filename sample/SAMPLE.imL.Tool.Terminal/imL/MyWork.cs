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
            _info.PathAttachments.Add(@"E:\20220419-operar_relojes.log");
            _info.PathAttachments.Add(@"E:\6658439.pdf");
            _info.PathAttachments.Add(@"E:\tmp\008060cbe886e344da4802aa8dec50d8.webp");
            _info.PathAttachments.Add(@"E:\tmp\01cc6b64479a5c613958b8c57b5e3bc9.mp4");
            //_info.PathAttachments.Add(@"E:\c493a066-77ca-46f2-a569-196041715319.html");
            //_info.PathAttachments.Add(@"E:\c493a066-77ca-46f2-a569-196041715319.htm");
            _info.PathAttachments.Add(@"E:\Lista completa de tipos MIME - HTTP _ MDN.mhtml");
            _logger.Info("Bye process");
        }
    }
}