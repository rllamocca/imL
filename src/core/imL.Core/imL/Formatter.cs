#if (NET35_OR_GREATER || NETSTANDARD1_2_OR_GREATER || NET5_0_OR_GREATER)

using System;
using System.Collections.Generic;
using System.Linq;

namespace imL
{
    public class Formatter
    {
        readonly string _FORMAT;
        readonly bool _DRAW_RL;
        readonly char[] _L_R;
        readonly char[] _R_R;
        readonly IList<KeyValuePair<int, string>> _FORMATS = new List<KeyValuePair<int, string>>();

        public Formatter(string _format, bool _draw_rl = true, char[] _l_r = null, char[] _r_r = null)
        {
            if (_draw_rl == false)
                _format = new string(_format.Reverse().ToArray());

            _FORMAT = _format;
            _DRAW_RL = _draw_rl;
            _L_R = _l_r;
            _R_R = _r_r;
        }
        /*
Before
During
After
        void BeforeExecution();
        void DuringExecution();
        void AfterExecution();
         */

        public string Enforce(string _value)
        {
            if (string.IsNullOrEmpty(_value))
                return _value;

            if (_L_R != null)
            {
                char[] _chars = _value.ToArray();
                bool _c = true;

                for (int _i = 0;
                    _i < _chars.Length;
                    _i++)
                {
                    if (_c && _L_R.Contains(_chars[_i]))
                        _chars[_i] = ' ';
                    else
                        _c = false;
                }

                _value = new string(_chars).Trim();
            }

            if (_R_R != null)
            {
                char[] _chars = _value.ToArray();
                bool _c = true;

                for (int _i = _chars.Length - 1;
                    _i == 0;
                    _i--)
                {
                    if (_c && _R_R.Contains(_chars[_i]))
                        _chars[_i] = ' ';
                    else
                        _c = false;
                }

                _value = new string(_chars).Trim();
            }

            KeyValuePair<int, string> _pair = _FORMATS.Where(_w => _w.Key == _value.Length).FirstOrDefault();
            string _format;

            if (_pair.Key == 0)
                _format = Analyze(_value.Length);
            else
                _format = _pair.Value;

            if (_DRAW_RL == false)
                _value = new string(_value.Reverse().ToArray());

            string _return = string.Format(_format, _value.Select(_s => Convert.ToString(_s)).ToArray());

            if (_DRAW_RL == false)
                _return = new string(_return.Reverse().ToArray());

            return _return;
        }
        string Analyze(int _length)
        {
            string _return = _FORMAT;

            IList<int> _indexs = new List<int>();

            for (int _i = 0;
                _i < _return.Length;
                _i++)
            {
                if (_return[_i] == '#' && _indexs.Count() < _length)
                    _indexs.Add(_i);
            }

            _return = _return.Substring(0, _indexs.Last() + 1);

            for (int _i = _return.Length - 1;
                _i >= 0;
                _i--)
            {
                if (_indexs.Contains(_i))
                    _return = _return.Remove(_i, 1).Insert(_i, "{" + Convert.ToString(_indexs.IndexOf(_i)) + "}");
            }

            _FORMATS.Add(new KeyValuePair<int, string>(_length, _return));

            return _return;
        }
    }
}

#endif