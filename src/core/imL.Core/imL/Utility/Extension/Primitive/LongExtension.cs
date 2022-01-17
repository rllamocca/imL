using System;

using imL.Enumeration;

namespace imL.Utility
{
    public static class LongExtension
    {
        public static string ToSize(this long _this, ESize _unit)
        {
            double _double = _this / Math.Pow(1024, (short)_unit);
            _double = Math.Round(_double, 8);

            return string.Format("{0} {1}", _double, _unit);
        }
    }
}
