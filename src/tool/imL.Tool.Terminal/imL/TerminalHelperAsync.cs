﻿#if (NET35 || NET40) == false

#if (NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)
using System.Text.Json;
#else
using Newtonsoft.Json;
#endif

using System;
using System.Threading.Tasks;

using imL.Contract;
using imL.Utility;

using NLog;

using imL.Package.NLog;
using imL.Enumeration;

namespace imL.Tool.Terminal
{
    public static class TerminalHelperAsync
    {
        public static async Task RunAsync<G>(Func<IProcessInfo, ISetting, Task> _dowork, string[] _args, string _href = "#", string _by = "404")
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
                    await _dowork(_proc, _sett);

                    _proc.Success();
                }
                catch (Exception _ex)
                {
                    _logger?.InnerFatal(_ex);
                    TerminalHelper.I__CATCH(_proc, _sett, _ex);
                }

                TerminalHelper.I__TRYN__(_proc, _sett, _href, _by);
                await SmtpHelperAsync.SendAsync(_sett.Smtp, _sett.Mail);
            }
            catch (Exception _ex)
            {
                if (_logger == null)
                    ConsoleHelper.InnerException(_ex);
                else
                    _logger?.InnerFatal(_ex);
            }
            finally
            {
                TerminalHelper.I__FINALLY();
            }
        }
    }
}

#endif