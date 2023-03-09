using System;

using NLog;

using imL.Package.NLog;

namespace imL.Tool.Terminal
{
    public static partial class TerminalHelper
    {
        public static void Run<G>(Action<IProcessInfo, ISetting> _dowork, string[] _args, string _href = "#", string _by = "404")
            where G : class, ISetting
        {
            try
            {
                IProcessInfo _info = I__TRY0_(_args);
                ISetting _sett = I__TRY1_<G>(_info);

                try
                {
                    _LOGGER?.Info("Run BEGIN");
                    _dowork(_info, _sett);

                    _info.Success();
                }
                catch (Exception _ex)
                {
                    _LOGGER?.InnerFatal(_ex);
                    I__CATCH_(_info, _sett, _ex);
                }

                _LOGGER?.Info("END Run");

                if (_MAIL)
                {
                    I__PRE_SEND_(_info, _sett, _href, _by);
                    SmtpHelper.Send(_sett.Smtp, _sett.Mail);
                    _LOGGER?.Info("Send MAIL");
                }
            }
            catch (Exception _ex)
            {
                if (_LOGGER == null) ConsoleHelper.WriteInnerException(_ex);
                else _LOGGER?.InnerFatal(_ex);

                throw;
            }
            finally
            {
                I__FINALLY_();
            }
        }
    }
}