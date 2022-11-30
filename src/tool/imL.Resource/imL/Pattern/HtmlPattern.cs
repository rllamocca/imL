using System;
using System.Collections.Generic;
using System.Linq;

using imL.Contract;
using imL.Enumeration;
using imL.Resource.Properties;
using imL.Utility;

namespace imL.Resource
{
    public static class HtmlPattern
    {
        public static string Resume(IProcessInfo _process, string _href = null, string _by = null)
        {
            if (_href == null && _by == null)
            {
                _href = "mailto:r.llamocca@outlook.com?Subject=INeedHelp";
                _by = "imL";
            }

            string _return = Resources.HTML_resume;

            //return _return;

            switch (_process.Alert)
            {
                case EAlert.Warning:
                    _return = _return.Replace("__ALERT__", Resources.HTML_warning);

                    break;
                case EAlert.Danger:
                    _return = _return.Replace("__ALERT__", Resources.HTML_danger);

                    break;
                case EAlert.Success:
                    _return = _return.Replace("__ALERT__", Resources.HTML_success);

                    break;
                case EAlert.Info:
                    _return = _return.Replace("__ALERT__", Resources.HTML_info);

                    break;
                default:
                    _return = _return.Replace("__ALERT__", "");

                    break;
            }

            if (_process.Critical != null)
            {
                IEnumerable<string> _critical = ExceptionHelper.InnerException(_process.Critical);
                _return = _return.Replace("__CRITICAL__", string.Join(Environment.NewLine, _critical.ToArray()));
            }
            else
                _return = _return.Replace("__CRITICAL__", "");

            _return = _return.Replace("__START__", Convert.ToString(_process.Start?.ToLocalTime()));
            _return = _return.Replace("__END__", Convert.ToString(_process.End?.ToLocalTime()));
            _return = _return.Replace("__DURATION__", Convert.ToString(_process.Duration()));
            _return = _return.Replace("__SELECTED__", Convert.ToString(_process.Selected));
            _return = _return.Replace("__INSERTED__", Convert.ToString(_process.Inserted));
            _return = _return.Replace("__UPDATED__", Convert.ToString(_process.Updated));
            _return = _return.Replace("__ERASED__", Convert.ToString(_process.Erased));
            _return = _return.Replace("__SUCCESSES__", Convert.ToString(_process.Successes));
            _return = _return.Replace("__ERRORS__", Convert.ToString(_process.Errors));
            _return = _return.Replace("__POWERED_HREF__", _href);
            _return = _return.Replace("__POWERED_BY__", _by);

            _return = _return.Replace("/*__STYLE__*/", Resources.CSS_bootstrap);

            return _return;
        }
    }
}
