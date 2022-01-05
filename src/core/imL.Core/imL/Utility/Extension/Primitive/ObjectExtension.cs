using System;

namespace imL.Utility
{
    public static class ObjectExtension
    {
        public static bool HasValue(this object _this)
        {
            return (_this != null);
        }
        public static string DBToString(this object _this, bool _empty = false)
        {
            if (_this == null
#if (NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3|| NETSTANDARD1_4 || NETSTANDARD1_5|| NETSTANDARD1_6) == false
                || _this == DBNull.Value
#endif
                )
            {
                if (_empty)
                    return string.Empty;

                return null;
            }

            return Convert.ToString(_this);
        }
        public static string[] DBToString(this object[] _array, bool _empty = false)
        {
            if (_array == null)
                return null;

            string[] _return = new string[_array.Length];

            for (int _i = 0; _i < _array.Length; _i++)
                _return[_i] = _array[_i].DBToString(_empty);

            return _return;
        }
    }
}
