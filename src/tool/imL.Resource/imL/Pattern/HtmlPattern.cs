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

            switch (_process.Alert)
            {
                case EAlert.Success:
                    

                    break;
                case EAlert.Warning:
                    

                    break;
                case EAlert.Danger:
                    

                    break;
                default:
                    _return = _return.Replace("__ALERT__", "");

                    break;
            }

            if (_process.Alert == EAlert.Danger)
            {
                if (_process.Critical != null)
                {
                    string _critical;

                    try
                    {
#if (NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)
                        _critical = JsonSerializer.Serialize(_process.Critical, new JsonSerializerOptions() { WriteIndented = true });
#else
                        _critical = JsonConvert.SerializeObject(_process.Critical, Formatting.Indented);
#endif
                    }
                    catch (Exception _ex)
                    {
                        _critical = _process.Critical.Message + Environment.NewLine + _ex.Message;
                    }

                    _return = _return.Replace("__CRITICAL__", _critical);
                }

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
