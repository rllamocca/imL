using System;
using System.Collections.Generic;

namespace imL
{
    public static class StructExtension
    {
        static readonly char _HT = (char)9;
        static readonly char _LF = (char)10;
        static readonly char _VT = (char)11;
        static readonly char _FF = (char)12;
        static readonly char _CR = (char)13;

        public static IList<char> RemoveEndLine(this IList<char> _array, EEndLine _el)
        {
            if (_array == null)
                return null;

            if (_array.Count > 0)
            {
                if (_el.HasFlag(EEndLine.HT)) _array.Remove(_HT);
                if (_el.HasFlag(EEndLine.LF)) _array.Remove(_LF);
                if (_el.HasFlag(EEndLine.VT)) _array.Remove(_VT);
                if (_el.HasFlag(EEndLine.FF)) _array.Remove(_FF);
                if (_el.HasFlag(EEndLine.CR)) _array.Remove(_CR);
            }

            return _array;
        }

        public static string[] ConvertToString(this char[] _array)
        {
            if (_array == null)
                return null;

            string[] _return = new string[_array.Length];

            for (int _i = 0; _i < _return.Length; _i++)
                _return[_i] = Convert.ToString(_array[_i]);

            return _return;
        }

        public static DateTime ToExcelTime(this TimeSpan _this)
        {
            return new DateTime(1899, 12, 31).AddTicks(_this.Ticks);
        }
        public static DateTime TimeStampToDateTime(this double _this)
        {
            return ReadOnly._TIMESTAMP.AddSeconds(_this);
        }

        public static IEnumerable<sbyte> GetUnits(this long _long, sbyte _base = 10)
        {
            if (_long == 0)
                yield return 0;

            while (_long != 0)
            {
                yield return Convert.ToSByte(_long % _base);

                _long /= _base;
            }
        }
    }
}
