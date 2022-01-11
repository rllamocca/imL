#if (NET35 || NET40) == false

#if (NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)
using System.Text.Json;
#else
using Newtonsoft.Json;
#endif

using System;
using System.IO;

using imL.Contract;
using imL.Resource;
using imL.Utility;

using NLog;

using System.Threading.Tasks;
using System.Net.Mail;

namespace imL.Tool.Terminal
{
    public static class TerminalHelperAsync
    {
        public static async Task RunAsync<G>(Func<IProcessInfo, ISettings, Task> _dowork, string[] _args, string _href = "#", string _by = "404")
            where G : class, ISettings
        {
#if DEBUG
            Utility.TerminalHelper.Starts();
#endif

            IProcessInfo _process = new ProcessInfoDefault(new AppInfoDefault(_args));
            LogManager.AutoShutdown = true;
            LogManager.Configuration.Variables["_BASEDIR_"] = _process.App.PathLog;
            LogManager.Configuration.Variables["_FILENAME_"] = _process.Guid;
            Logger _logger = LogManager.GetCurrentClassLogger();

            try
            {
#if (NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)
                ISettings _sett = JsonSerializer.Deserialize<G>(File.ReadAllText(Path.Combine(_process.App.Path, "settings.json")));
#else
                ISettings _sett = JsonConvert.DeserializeObject<G>(File.ReadAllText(Path.Combine(_process.App.Path, "settings.json")));
#endif

                try
                {
                    await _dowork(_process, _sett);
                    _process.Success();
                }
                catch (Exception _ex)
                {
                    _logger.Fatal(_ex);
                    _process.Danger(_ex);
                    _sett.Mail.Priority = MailPriority.High;
                }

                _sett.Mail.IsBodyHtml = true;
                _sett.Mail.Body = HtmlPattern.Resume(_process, _href, _by);
                _sett.Mail.PathAttachments = new string[] { Path.Combine(_process.App.PathLog, _process.Guid + ".log") };

                LogManager.Shutdown();
                await SmtpHelperAsync.SendAsync(_sett.Smtp, null, _sett.Mail);
            }
            catch (Exception _ex)
            {
                Console.WriteLine(_ex);
                _logger.Fatal(_ex);
            }
            finally
            {
                LogManager.Shutdown();
            }
#if DEBUG
            Utility.TerminalHelper.Ends(true);
#endif
        }
    }
}

#endif