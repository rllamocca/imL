using imL;
using imL.Package.NLog;
using imL.Tool.Terminal;

using NLog;

namespace SAMPLE.imL.Tool.Terminal
{
    internal class MyWork
    {
        public static async Task DoWork(IProcessInfo _info, ISetting _settings)
        {
            ILogger? _logger = LogManager.GetCurrentClassLogger();
            MySetting _setting = (MySetting)_settings;

            _logger?.LetDebug()?.Debug("Debug");

            await Task.Delay(1000);
            Guid _guid;

            for (int _i = 0; _i < 500; _i++)
            {
                await Task.Delay(10);
                _guid = Guid.NewGuid();
                _logger?.Info("{0}) {1}", _i.ToString("0000"), _guid);
            }

            await Task.Delay(2000);

            //_info.PathAttachments.Add(@"E:\20220419-operar_relojes.log");
            //_info.PathAttachments.Add(@"E:\6658439.pdf");
            //_info.PathAttachments.Add(@"E:\tmp\008060cbe886e344da4802aa8dec50d8.webp");
            //_info.PathAttachments.Add(@"E:\tmp\01cc6b64479a5c613958b8c57b5e3bc9.mp4");
            //_info.PathAttachments.Add(@"E:\c493a066-77ca-46f2-a569-196041715319.html");
            //_info.PathAttachments.Add(@"E:\c493a066-77ca-46f2-a569-196041715319.htm");
            //_info.PathAttachments.Add(@"E:\Lista completa de tipos MIME - HTTP _ MDN.mhtml");
        }
    }
}