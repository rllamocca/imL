using System;
using System.Collections.Generic;
using System.Linq;

namespace imL
{
    public class Password : IDisposable
    {
        //؟
        bool _DISPOSED = false;

        const ERandomSort _SORT = ERandomSort.Fisher_Yates;
        char[] _BASE;
        char[] _GENERATED;

        public bool Numbers { set; get; } = true;
        public bool UpperCase { set; get; } = true;
        public bool LowerCase { set; get; } = true;
        public bool Specials { set; get; } = false;
        public char[] Aggregate { set; get; }

        public string Base { get { return new string(_BASE); } }
        public string Generated { get { return new string(_GENERATED); } }

        public Password(
            bool _numbers = true,
            bool _uppercase = true,
            bool _lowercase = true,
            bool _specials = false,
            char[] _aggregate = null
            )
        {
            Numbers = _numbers;
            UpperCase = _uppercase;
            LowerCase = _lowercase;
            Specials = _specials;
            Aggregate = _aggregate;
        }
        public void Generate(byte _length = 8)
        {
            _BASE = Password.Prepare(Numbers, UpperCase, LowerCase, Specials, Aggregate, _length);
            _GENERATED = Password.Generate(_BASE, _length);
        }

        public static char[] Prepare(
            bool _numbers = true,
            bool _uppercase = true,
            bool _lowercase = true,
            bool _specials = false,
            char[] _aggregate = null,
            byte _length = 8
            )
        {
            List<char> _tmp = new List<char>();
            if (_numbers) _tmp.AddRange(ReadOnly._NUMBERS.RandomSort(Password._SORT).Take(_length));
            if (_uppercase) _tmp.AddRange(ReadOnly._UPPERCASE.RandomSort(Password._SORT).Take(_length));
            if (_lowercase) _tmp.AddRange(ReadOnly._LOWERCASE.RandomSort(Password._SORT).Take(_length));
            if (_specials) _tmp.AddRange(ReadOnly._SPECIALS.RandomSort(Password._SORT).Take(_length));
            if (_aggregate != null) _tmp.AddRange(_aggregate.RandomSort(Password._SORT));

            IList<char> _tmp2 = _tmp.Distinct().ToList();
            _tmp2 = _tmp2.RemoveEndLine(EEndLine.All);

            _tmp2 = _tmp2.RandomSort(Password._SORT);

            return _tmp2.ToArray();
        }
        public static char[] Generate(char[] _base, byte _length = 8)
        {
            char[] _return = new char[_length];
            int _max = _base.Length;
            Random _r = new Random();
            for (byte _i = 0; _i < _return.Length; _i++)
                _return[_i] = _base[_r.Next(_max)];

            return _return;
        }

        //################################################################################
        ~Password()
        {
            Dispose(false);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool _managed)
        {
            if (_DISPOSED)
                return;

            if (_managed)
            {
                _BASE = null;
                _GENERATED = null;

                LowerCase = false;
                UpperCase = false;
                Numbers = false;
                Specials = false;
                Aggregate = null;
            }

            _DISPOSED = true;
        }
    }
}