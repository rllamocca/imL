#if (NET35_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)

using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;

using imL.Enumeration;
using imL.Utility;

namespace imL
{
    public static class Analyzer
    {
        static readonly string[] _DATETIME_FORMATs = new string[] {
                "yyyyMMdd",
                "yyyy'-'MM'-'dd",
                "yyyy'-'MM'-'dd HH':'mm':'ss",
                "ddMMyyyy",
                "dd'/'MM'/'yyyy"
            };

        static readonly string _LF = new string(new char[] { (char)10 });
        static readonly string _CR = new string(new char[] { (char)13 });

        public static string GetStringNull(string _key, string _value, int _length = 0, params string[] _in)
        {
            if (_length > 0 && _value.Length > _length) throw new HandledException(string.Format("{0} con largo máximo superado; ({1})", _key, _length));

            if (_in.Length > 0)
            {
                if (_in.Contains(_value, StringComparer.OrdinalIgnoreCase) == false)
                    throw new HandledException(string.Format("{0} no válido", _key));
            }

            return _value.Trim(); //¿?
        }
        public static string GetString(string _key, string _value, int _length = 0, params string[] _in)
        {
            if (string.IsNullOrEmpty(_value)) throw new HandledException(string.Format("{0} no informado", _key));

            return GetStringNull(_key, _value, _length, _in);
        }
        public static bool GetBool(string _key, string _value, int _length = 0, params string[] _in)
        {
            _value = GetString(_key, _value, _length, _in);

            if (bool.TryParse(_value, out bool _return))
                return _return;

            throw new HandledException(string.Format("{0} no representa un número", _key));
        }
        public static sbyte GetSByte(string _key, string _value, int _length = 0, ENumberLine _nl = ENumberLine.None, params string[] _in)
        {
            _value = GetString(_key, _value, _length, _in);

            if (sbyte.TryParse(_value, out sbyte _return))
            {
                switch (_nl)
                {
                    case ENumberLine.Negative: if (0 < _return) throw new HandledException(string.Format("{0} con valor positivo", _key)); break;
                    case ENumberLine.Positive: if (_return < 0) throw new HandledException(string.Format("{0} con valor negativo", _key)); break;
                    default:
                        break;
                }

                return _return;
            }

            throw new HandledException(string.Format("{0} no representa un número", _key));
        }
        public static short GetShort(string _key, string _value, int _length = 0, ENumberLine _nl = ENumberLine.None, params string[] _in)
        {
            _value = GetString(_key, _value, _length, _in);

            if (short.TryParse(_value, out short _return))
            {
                switch (_nl)
                {
                    case ENumberLine.Negative: if (0 < _return) throw new HandledException(string.Format("{0} con valor positivo", _key)); break;
                    case ENumberLine.Positive: if (_return < 0) throw new HandledException(string.Format("{0} con valor negativo", _key)); break;
                    default:
                        break;
                }

                return _return;
            }

            throw new HandledException(string.Format("{0} no representa un número", _key));
        }
        public static int GetInt(string _key, string _value, int _length = 0, ENumberLine _nl = ENumberLine.None, params string[] _in)
        {
            _value = GetString(_key, _value, _length, _in);

            if (int.TryParse(_value, out int _return))
            {
                switch (_nl)
                {
                    case ENumberLine.Negative: if (0 < _return) throw new HandledException(string.Format("{0} con valor positivo", _key)); break;
                    case ENumberLine.Positive: if (_return < 0) throw new HandledException(string.Format("{0} con valor negativo", _key)); break;
                    default:
                        break;
                }

                return _return;
            }

            throw new HandledException(string.Format("{0} no representa un número", _key));
        }
        public static long GetLong(string _key, string _value, int _length = 0, ENumberLine _nl = ENumberLine.None, params string[] _in)
        {
            _value = GetString(_key, _value, _length, _in);

            if (long.TryParse(_value, out long _return))
            {
                switch (_nl)
                {
                    case ENumberLine.Negative: if (0 < _return) throw new HandledException(string.Format("{0} con valor positivo", _key)); break;
                    case ENumberLine.Positive: if (_return < 0) throw new HandledException(string.Format("{0} con valor negativo", _key)); break;
                    default:
                        break;
                }

                return _return;
            }

            throw new HandledException(string.Format("{0} no representa un número", _key));
        }
        public static DateTime GetDateTime(string _key, string _value, int _length = 0, params string[] _in)
        {
            _value = GetString(_key, _value, _length, _in);

            if (DateTime.TryParseExact(_value, _DATETIME_FORMATs, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out DateTime _return))
                return _return;

            throw new HandledException(string.Format("{0} no representa una fecha", _key));
        }

        public static void CheckCount(string _key, int _count, int _min = 0, int _max = 0, params int[] _in)
        {
            if (_min > 0 && _count < _min) throw new HandledException(string.Format("{0} con menos detalles que los esperados; ({1})", _key, _count));
            if (_max > 0 && _max < _count) throw new HandledException(string.Format("{0} con más detalles que los esperados; ({1})", _key, _count));

            if (_in.Length > 0)
            {
                if (_in.Contains(_count) == false)
                    throw new HandledException(string.Format("{0} no válido, cantidad de detalles distinta a la informada; ({1})", _key, _count));
            }
        }
        public static void CheckSum(string _key, long _value, long _sum)
        {
            if (_value != _sum)
                throw new HandledException(string.Format("el {0} informado no corresponde al acumulado, ({1}) ({2})", _key, _value, _sum));
        }
        public static void CheckLess(string _key1, string _key2, long _value1, long _value2)
        {
            if (_value1 < _value2)
                throw new HandledException(string.Format("el valor de {0} no debe ser menor al valor de {1}", _key1, _key2));
        }
        public static void CheckRUN(string _key, string _run, int _max = 0)
        {
            int _a = _run.Length - 1;

#if NETSTANDARD2_1_OR_GREATER
            long _rut = Convert.ToInt64(_run[.._a]);
#else
            long _rut = Convert.ToInt64(_run.Substring(0, _a));
#endif

            if (_max > 0)
            {
                string _string = Convert.ToString(_rut);

                if (_string.Length > _max)
                    throw new HandledException("{0}, RUT con largo máximo superado", _key);
            }

            if (RUNIsValid(_rut, _run.Substring(_a, 1)) == false)
                throw new HandledException(string.Format("{0}, el digito verificador no corresponde al RUT informado", _key));
        }
        public static void CheckModule(string _key, int _length, int _mod)
        {
            if (_length == 0)
                throw new HandledException(string.Format("{0} no informado", _key));

            if (_length % _mod != 0)
                throw new HandledException(string.Format("{0} con largo no apropiado para cortes de {1}", _key, _mod));
        }

        public static string RUTGetDV(long _rut)
        {
            if (_rut <= 0)
                throw new ArgumentOutOfRangeException(nameof(_rut));

            int _dv = 0;
            sbyte _factor = 2;

            foreach (sbyte _item in _rut.GetUnits())
            {
                _dv += _item * _factor;
                _factor++;

                if (_factor >= 8)
                    _factor = 2;
            }

            _dv = 11 - (_dv % 11);

            switch (_dv)
            {
                case 10: return "K";
                case 11: return "0";
                default: return Convert.ToString(_dv);
            }
        }
        public static bool RUNIsValid(long _rut, string _dv, int _max = 0)
        {
            if (_max > 0)
            {
                string _string = Convert.ToString(_rut);

                if (_string.Length > _max)
                    return false;
            }

            string _run = string.Format("{0}-{1}", _rut, _dv);
            Regex _reg = new Regex("[0-9]+-[0-9Kk]");

            if (_reg.IsMatch(_run) == false)
                return false;

            return _dv.Equals(RUTGetDV(_rut), StringComparison.OrdinalIgnoreCase);
        }

        public static string CleanEndLine(string _value)
        {
            //char _null = (char)0;
            //char _lf = (char)10;
            //char _cr = (char)13;

            return _value.Replace(_LF, string.Empty).Replace(_CR, string.Empty);
        }

        public static XmlDocument LoadXml(string _xml, bool _normalize = true)
        {
            XmlDocument _return = new XmlDocument();
            _return.LoadXml(_xml);

            if (_normalize)
                _return.Normalize();

            return _return;
        }
        public static void AddXmlElement(XmlDocument _doc, XmlNode _nod, string _key, object _value = null, string _attribute = null)
        {
            XmlElement _xe = _doc.CreateElement(_key);

            if (_value != null)
            {
                if (_value is string _string)
                    _xe.InnerText = _string;
                else
                    _xe.InnerText = Convert.ToString(_value);
            }

            if (_attribute != null)
            {
                XmlAttribute _xa = _doc.CreateAttribute("xsi", "type", "http://www.w3.org/2001/XMLSchema-instance");
                _xa.Value = _attribute;
                _xe.Attributes.Append(_xa);
            }

            _nod.AppendChild(_xe);
        }
    }
}

#endif