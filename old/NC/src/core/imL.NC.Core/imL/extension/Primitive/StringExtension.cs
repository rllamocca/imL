#if (NET35_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
#endif

//#if NETSTANDARD1_0 || NETSTANDARD1_1
//using System.Collections.Generic;
//#endif

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace imL
{
    public static class StringExtension
    {
        public static bool HasValue(this string _this)
        {
            return (_this != null);
        }
        public static bool HasValueLength(this string _this)
        {
            return (_this.HasValue() && _this.Length > 0);
        }
        public static bool HasValueTrim(this string _this)
        {
            return (_this.HasValue() && _this.Trim().Length > 0);
        }
        public static string ReplaceEndLine(this string _this, EEndLine _re)
        {
            if (_this == null)
                return null;

            if (_this.Length > 0)
            {
                char _sp = (char)32;

                char _ht = (char)9;
                char _lf = (char)10;
                char _vt = (char)11;
                char _ff = (char)12;
                char _cr = (char)13;

                if (_re.HasFlag(EEndLine.HT)) _this = _this.Replace(_ht, _sp);
                if (_re.HasFlag(EEndLine.LF)) _this = _this.Replace(_lf, _sp);
                if (_re.HasFlag(EEndLine.VT)) _this = _this.Replace(_vt, _sp);
                if (_re.HasFlag(EEndLine.FF)) _this = _this.Replace(_ff, _sp);
                if (_re.HasFlag(EEndLine.CR)) _this = _this.Replace(_cr, _sp);
            }

            return _this;
        }
        public static bool ArgAppear(this string[] _array, params string[] _params)
        {
            if (_array == null)
                return false;

            bool _return = false;

            for (int _i = 0; _i < _params.Length; _i++)
            {
                //_return = _return || (_array.Count(_c => _c.CompareTo(_item) == 1) > 0);
                //_return = _return || (_array.Count(_c => _c.ToUpper() == _synonym[_i].ToUpper()) > 0);
                _return = _return || (_array.Any(_a => _a.Equals(_params[_i], StringComparison.OrdinalIgnoreCase)));

                if (_return)
                    return _return;
            }

            return _return;
        }
        public static string ArgValue(this string[] _array, params string[] _params)
        {
            if (_array == null)
                return null;

            for (int _i = 0; _i < _params.Length; _i++)
            {
                string _return = _array.SkipWhile(_sw => _sw.ToUpper() != _params[_i].ToUpper()).Skip(1).FirstOrDefault();

                if (_return != null)
                    return _return;
            }

            return null;
        }
        public static string Replace(this string _this, char _new, params char[] _params)
        {
            if (_this == null)
                return null;

            if (_params.IsEmpty())
                return _this;

            string _return = _this;
            _params = _params.Distinct().ToArray();

            for (int _i = 0; _i < _params.Length; _i++)
                _return = _return.Replace(_params[_i], _new);

            return _return;
        }
        public static string Replace(this string _this, string _new, params string[] _params)
        {
            if (_this == null)
                return null;

            if (_params.IsEmpty())
                return _this;

            string _return = _this;
            _params = _params.Distinct().ToArray();

            for (int _i = 0; _i < _params.Length; _i++)
                _return = _return.Replace(_params[_i], _new);

            return _return;
        }
        public static void ToStream(this string _this, out Stream _out, Encoding _enc = null)
        {
            ReadOnly.DefaultEncoding_NoBOM(ref _enc);

            _out = new MemoryStream();
            StreamWriter _sw = new StreamWriter(_out, _enc);
            _sw.Write(_this);
            _sw.Flush();
        }

#if (NET35_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)
        public static bool IsMail(this string _this, bool _throw = false)
        {
            if (string.IsNullOrEmpty(_this))
                return false;

            try
            {
                _ = new MailAddress(_this);

                return true;
            }
            catch (Exception)
            {
                if (_throw)
                    throw;
            }

            return false;
        }
#endif

        public static string ToLetterOrDigit(this string _this, params char[] _params)
        {
            if (_this == null)
                return null;

#if NETSTANDARD1_0 || NETSTANDARD1_1
            IList<char> _return = new List<char>();
            
            foreach (char _item in _this)
                if (char.IsLetterOrDigit(_item) || _let.Contains(_item))
                    _return.Add(_item);

            return new string(_return.ToArray());
#else
            return new string(_this.Where(_w => char.IsLetterOrDigit(_w) || _params.Contains(_w)).ToArray());
#endif

        }


        public static IEnumerable<string> GetCutsISync(string _string, int _length)
        {
            if (_length <= 0)
                yield break;

            while (_string.Length > 0)
            {
#if NETSTANDARD2_1_OR_GREATER
                yield return _string[.._length];
#else
                yield return _string.Substring(0, _length);
#endif

                _string = _string.Remove(0, _length);

            }
        }
    }
}