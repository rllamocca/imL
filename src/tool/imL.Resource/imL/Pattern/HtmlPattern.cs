#if (NETSTANDARD2_0_OR_GREATER ||  NET5_0_OR_GREATER)
using System.Text.Json;
#else
using Newtonsoft.Json;
#endif

using imL.Contract;
using imL.Enumeration;
using imL.Resource.Properties;

using System;

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

            switch (_process.Alert)
            {
                case EAlert.Success:
                    _return = _return.Replace("__ALERT__", Resources.HTML_success);

                    break;
                case EAlert.Warning:
                    _return = _return.Replace("__ALERT__", Resources.HTML_warning);

                    break;
                case EAlert.Danger:
                    _return = _return.Replace("__ALERT__", Resources.HTML_danger);

                    break;
                default:
                    _return = _return.Replace("__ALERT__", "");

                    break;
            }

            if (_process.Alert == EAlert.Danger)
#if (NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)
                _return = _return.Replace("__CRITICAL__", JsonSerializer.Serialize(_process.Critical, new JsonSerializerOptions() { WriteIndented = true }));
#else
                _return = _return.Replace("__CRITICAL__", JsonConvert.SerializeObject(_process.Critical, Formatting.Indented));
#endif
            else
                _return = _return.Replace("__CRITICAL__", "");

            _return = _return.Replace("__START__", Convert.ToString(_process.Start?.ToLocalTime()));
            _return = _return.Replace("__END__", Convert.ToString(_process.End?.ToLocalTime()));
            _return = _return.Replace("__DURATION__", Convert.ToString(_process.Duration()));
            _return = _return.Replace("__SELECTED__", Convert.ToString(_process.Selected));
            _return = _return.Replace("__INSERTED__", Convert.ToString(_process.Inserted));
            _return = _return.Replace("__UPDATED__", Convert.ToString(_process.Updated));
            _return = _return.Replace("__ERASED__", Convert.ToString(_process.Erased));
            _return = _return.Replace("__POWERED_HREF__", _href);
            _return = _return.Replace("__POWERED_BY__", _by);

            _return = _return.Replace("/*__STYLE__*/", Resources.CSS_bootstrap);

            return _return;
        }
    }
}
