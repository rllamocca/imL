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

using imL.Package.NLog;

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
        internal static void I__TRYN__(IProcessInfo _process, ISetting _settings, string _href, string _by)
        {
            _settings.Mail.Encoding = "utf-8";
            _settings.Mail.IsBodyHtml = true;
            _settings.Mail.Body = HtmlPattern.Resume(_process, _href, _by);

            string _log = Path.Combine(_process.App.PathLog, _process.Guid + ".log");
            //string _html = Path.Combine(_process.App.PathOut, _process.Guid + ".html");
            //string _pdf = Path.Combine(_process.App.PathOut, _process.Guid + ".pdf");

            //File.WriteAllText(_html, _settings.Mail.Body);

            List<string> _acum = new List<string>();
            _acum.Add(_log);
            //_acum.Add(_html);
            //_acum.Add(_pdf);
            _acum.AddRange(_process.PathAttachments.DefaultOrEmpty());
            _acum.AddRange(_settings.Mail.PathAttachments.DefaultOrEmpty());
            _acum = _acum.Distinct().ToList();
            List<string> _attachs = new List<string>();
            MemoryUnit _mb = default;

            if (_settings.Smtp.MinSizeZipAttachment > 0)
                _mb = new MemoryUnit(_settings.Smtp.MinSizeZipAttachment.GetValueOrDefault(), EMemoryUnit.MB);

            string[] _exts = new string[] { ".docx", ".xlsx", ".pdf" };

            foreach (string _item in _acum)
                _attachs.Add(ZipHelper.CompressOnly(_item, _mb, _exts));

            _settings.Mail.PathAttachments = _attachs;
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

                    _proc.Success();
                }
                catch (Exception _ex)
                {
                    _logger?.InnerFatal(_ex);
                    TerminalHelper.I__CATCH(_proc, _sett, _ex);
                }

                TerminalHelper.I__TRYN__(_proc, _sett, _href, _by);
                SmtpHelper.Send(_sett.Smtp, _sett.Mail);
            }
            catch (Exception _ex)
            {
                if (_logger == null)
                    ConsoleHelper.InnerException(_ex);
                else
                    _logger?.InnerFatal(_ex);

                throw;
            }
            finally
            {
                TerminalHelper.I__FINALLY();
            }
        }
    }
}