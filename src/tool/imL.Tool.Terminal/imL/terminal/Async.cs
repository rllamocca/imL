#if (NET35 || NET40) == false

using System;
using System.Threading.Tasks;

using NLog;

using imL.Package.NLog;

namespace imL.Tool.Terminal
{
    public static partial class TerminalHelper
    {
        public static async Task RunAsync<G>(Func<IProcessInfo, ISetting, Task> _dowork, string[] _args, string _href = "#", string _by = "404")
            where G : class, ISetting
        {
            try
            {
                IProcessInfo _info = I__TRY0_(_args);
                ISetting _sett = I__TRY1_<G>(_info);

                try
                {
                    _LOGGER?.Info("RunAsync BEGIN");
                    await _dowork(_info, _sett);

                    _info.Success();
                }
                catch (Exception _ex)
                {
                    _LOGGER?.InnerFatal(_ex);
                    I__CATCH_(_info, _sett, _ex);
                }

                _LOGGER?.Info("RunAsync END");

                if (_MAIL)
                {
                    I__PRE_SEND_(_info, _sett, _href, _by);
                    LogManager.Flush();
                    await SmtpHelper.SendAsync(_sett.Smtp, default, _sett.Mail);
                    _LOGGER?.Info("SendAsync MAIL");
                }
            }
            catch (Exception _ex)
            {
                if (_LOGGER == null)
                    ConsoleHelper.WriteInnerException(_ex);
                else
                    _LOGGER?.InnerFatal(_ex);
            }
            finally
            {
                I__FINALLY_();
            }
        }
    }
}

#endif