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

using imL.Resource;

using NLog;

namespace imL.Tool.Terminal
{
    public static partial class TerminalHelper
    {
        static ILogger _LOGGER = null;
        static bool _MAIL = true;

        internal static IProcessInfo I__TRY0_(string[] _args)
        {
            ConsoleHelper.Begins();
            IProcessInfo _return = new ProcessInfoDefault(new AppInfoDefault(_args), true);

            LogManager.Configuration.Variables["_BASEDIR_"] = _return.Base;
            LogManager.Configuration.Variables["_FILENAME_"] = _return.FileLog;
            LogManager.AutoShutdown = true;

            _LOGGER = LogManager.GetCurrentClassLogger();
            _LOGGER?.Info("TERMINAL BEGIN");

            return _return;
        }
        internal static ISetting I__TRY1_<G>(IProcessInfo _info)
            where G : class, ISetting
        {
#if (NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)
            G _return = JsonSerializer.Deserialize<G>(File.ReadAllText(Path.Combine(_info.App.Base, "settings.json")));
#else
            G _return = JsonConvert.DeserializeObject<G>(File.ReadAllText(Path.Combine(_info.App.Base, "settings.json")));
#endif

            if (_return == null)
                throw new ArgumentNullException("settings.json");

            if (_MAIL && _return.Smtp == null) _MAIL = false;
            if (_MAIL && _return.Mail == null) _MAIL = false;
            if (_MAIL && _return.Mail.TO.IsEmpty()) _MAIL = false;

            return _return;
        }
        internal static void I__PRE_SEND_(IProcessInfo _info, ISetting _setting, string _href, string _by)
        {
            _setting.Mail.Encoding = "utf-8";
            _setting.Mail.IsBodyHtml = true;
            _setting.Mail.Body = HtmlPattern.Resume(_info, _href, _by);

            string _log = Path.Combine(_info.Base, _info.FileLog);

            List<string> _acum = new List<string>();
            _acum.Add(_log);
            _acum.AddRange(_info.PathAttachments.DefaultOrEmpty());
            _acum.AddRange(_setting.Mail.PathAttachments.DefaultOrEmpty());
            _acum = _acum.Distinct().ToList();

            //List<string> _attachs = new List<string>();
            //MemoryUnit _mb = default;

            //if (_settings.Smtp.MinSizeZipAttachment > 0)
            //    _mb = new MemoryUnit(_settings.Smtp.MinSizeZipAttachment.GetValueOrDefault(), EMemoryUnit.MB);

            //string[] _exts = new string[] { ".docx", ".xlsx", ".pdf" };

            //foreach (string _item in _acum)
            //    _attachs.Add(ZipHelper.CompressOnly(_item, _mb, _exts));

            _setting.Mail.PathAttachments = _acum;
            LogManager.Flush();
        }
        internal static void I__CATCH_(IProcessInfo _info, ISetting _setting, Exception _ex)
        {
            _info.Danger(_ex);

            if (_MAIL)
                _setting.Mail.Priority = MailPriority.High;
        }
        internal static void I__FINALLY_()
        {
            _LOGGER?.Info("END TERMINAL");

            LogManager.Shutdown();
            ConsoleHelper.Ends(_card: true);
        }
    }
}