using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace imL
{
    public static class ReorderExtension
    {
        public static object TryGet(this DataRow _this, string _columname)
        {
            if (_this == null)
                return null;

            try
            {
                return _this[_columname];
            }
            catch
            {
                return default;
            }
        }
        public static string ToText(this object _this, bool _trim = true)
        {
            if (_this == null) return default;
            if (_this == default) return default;
#if (NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6) == false
            if (_this == DBNull.Value) return default;
#endif

            string _return = Convert.ToString(_this);

            if (_return == null) return null;
            if (_trim) return _return.Trim();

            return _return;
        }
        public static int ToInt32(this object _this)
        {
            if (_this == null) return default;
            if (_this == default) return default;
#if (NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6) == false
            if (_this == DBNull.Value) return default;
#endif

            return Convert.ToInt32(_this);
        }
        public static long ToInt64(this object _this)
        {
            if (_this == null) return default;
            if (_this == default) return default;
#if (NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6) == false
            if (_this == DBNull.Value) return default;
#endif

            return Convert.ToInt64(_this);
        }
        public static IEnumerable<string> Trim(this IEnumerable<string> _array)
        {
            return from _r in _array
                   select _r.Trim()
                   ;
        }

        public static G ToUnbox<G>(this object _this)
        {
            if (_this == null) return default;
            if (_this == default) return default;
#if (NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6) == false
            if (_this == DBNull.Value) return default;
#endif

            return (G)_this;
        }
        public static G ToChangeType<G>(this object _this)
        {
            if (_this == null) return default;
            if (_this == default) return default;
#if (NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6) == false
            if (_this == DBNull.Value) return default;
#endif

            if (typeof(G) == _this.GetType())
                return (G)_this;

            return (G)Convert.ChangeType(_this, typeof(G));
        }
    }
}
