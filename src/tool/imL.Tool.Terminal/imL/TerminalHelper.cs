#if (NETSTANDARD2_0_OR_GREATER ||  NET5_0_OR_GREATER)
using System.Text.Json;
#else
using Newtonsoft.Json;
#endif

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;

using imL.Contract;
using imL.Enumeration;
using imL.Package.Zip;
using imL.Resource;
using imL.Struct;
using imL.Utility;

using NLog;

namespace imL.Tool.Terminal
{
    public static class TerminalHelper
    {
        internal static IProcessInfo I__TRY0__(string[] _args)
        {
            ConsoleHelper.Begins();
            IProcessInfo _return = new ProcessInfoDefault(new AppInfoDefault(_args));
            LogManager.AutoShutdown = true;
            LogManager.Configuration.Variables["_BASEDIR_"] = _return.App.PathLog;
            LogManager.Configuration.Variables["_FILENAME_"] = _return.Guid;

            return _return;
        }
        internal static ISetting I__TRY1__<G>(IProcessInfo _process)
            where G : class, ISetting
        {
#if (NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)
            return JsonSerializer.Deserialize<G>(File.ReadAllText(Path.Combine(_process.App.Path, "settings.json")));
#else
            return JsonConvert.DeserializeObject<G>(File.ReadAllText(Path.Combine(_process.App.Path, "settings.json")));
#endif
        }
        internal static void I__TRY2__(IProcessInfo _process, ISetting _settings)
        {
            List<string> _acum = new List<string>();
            _acum.AddRange(_process.PathAttachments.DefaultOrEmpty());
            _acum.AddRange(_settings.Mail.PathAttachments.DefaultOrEmpty());
            _acum.Add(Path.Combine(_process.App.PathLog, _process.Guid + ".log"));
            _acum = _acum.Distinct().ToList();
            List<string> _attachs = new List<string>();

            MemoryUnit _mb = new MemoryUnit(1, EMemoryUnit.MB);
            string[] _exts = new string[] { ".txt", ".log", ".doc", ".xls" };

            foreach (string _item in _acum)
                _attachs.Add(ZipHelper.CompressOnly(_item, _mb, _exts));

            _settings.Mail.PathAttachments = _attachs;
            _process.Success();
        }
        internal static void I__TRYN__(IProcessInfo _process, ISetting _settings, string _href, string _by)
        {
            _settings.Mail.IsBodyHtml = true;
            _settings.Mail.Body = HtmlPattern.Resume(_process, _href, _by);
            LogManager.Shutdown();
        }
        internal static void I__CATCH(IProcessInfo _process, ISetting _settings, Exception _ex)
        {
            _process.Danger(_ex);
            _settings.Mail.Priority = MailPriority.High;
        }
        internal static void I__FINALLY()
        {
            LogManager.Shutdown();
            ConsoleHelper.Ends(_card: true);
        }

        public static void Run<G>(Action<IProcessInfo, ISetting> _dowork, string[] _args, string _href = "#", string _by = "404")
                    where G : class, ISetting
        {
            ILogger _logger = null;

            try
            {
                IProcessInfo _proc = TerminalHelper.I__TRY0__(_args);
                _logger = LogManager.GetCurrentClassLogger();
                ISetting _sett = TerminalHelper.I__TRY1__<G>(_proc);

                try
                {
                    _dowork(_proc, _sett);

                    TerminalHelper.I__TRY2__(_proc, _sett);
                }
                catch (Exception _ex)
                {
                    _logger.Fatal(_ex);
                    TerminalHelper.I__CATCH(_proc, _sett, _ex);
                }

                TerminalHelper.I__TRYN__(_proc, _sett, _href, _by);
                SmtpHelper.Send(_sett.Smtp, null, _sett.Mail);
            }
            catch (Exception _ex)
            {
                if (_logger == null)
                    Console.WriteLine(_ex);
                else
                    _logger.Fatal(_ex);

                throw;
            }
            finally
            {
                TerminalHelper.I__FINALLY();
            }
        }
    }
}