using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace imL
{
    public static class GenericExtension
    {
        public static G[] RandomSort<G>(this G[] _array, ERandomSort _sort = ERandomSort.None)
        {
            if (_array == null)
                return null;

            G[] _return = _array;
            Random _r = new Random();

            switch (_sort)
            {
                case ERandomSort.Fisher_Yates:
                    for (int _k = _return.Length - 1; _k > 0; _k--)
                    {
                        int _az = _r.Next(_k);

#if NETSTANDARD2_0_OR_GREATER || NETCOREAPP
                        (_return[_k], _return[_az]) = (_return[_az], _return[_k]);
#else
                        G _tmp = _return[_az];
                        _return[_az] = _return[_k];
                        _return[_k] = _tmp;
#endif

                    }
                    break;
                default:
                    break;
            }

            return _return;
        }
        public static List<G> RandomSort<G>(this List<G> _array, ERandomSort _sort = ERandomSort.None)
        {
            if (_array == null)
                return null;

            List<G> _return = _array;
            Random _r = new Random();

            switch (_sort)
            {
                case ERandomSort.Fisher_Yates:
                    for (int _k = _return.Count - 1; _k > 0; _k--)
                    {
                        int _az = _r.Next(_k);

#if NETSTANDARD2_0_OR_GREATER || NETCOREAPP
                        (_return[_k], _return[_az]) = (_return[_az], _return[_k]);
#else
                        G _tmp = _return[_az];
                        _return[_az] = _return[_k];
                        _return[_k] = _tmp;
#endif

                    }
                    break;
                default:
                    break;
            }

            return _return;
        }
        public static G ValueOrException<G>(this G? _this)
            where G : struct
        {
            if (_this.HasValue)
                return _this.Value;
            else
                throw new ArgumentNullException(nameof(_this));
        }

        public static IEnumerable<G> DefaultOrEmpty<G>(this IEnumerable<G> _array)
        {
            return _array ?? Enumerable.Empty<G>();
        }
        public static IEnumerable<G> ReturnAny<G>(this IEnumerable<G> _array, IEnumerable<G> _default = null)
        {
            return _array.Any() ? _array : _default;
        }
        public static bool HasValue<G>(this IEnumerable<G> _array)
        {
            return (_array != null);
        }
        public static bool IsEmpty<G>(this IEnumerable<G> _array)
        {
            return (_array == null || _array.Any() == false);
        }
        public static bool NotEmpty<G>(this IEnumerable<G> _array)
        {
            return (_array != null && _array.Any());
        }

#if (NET45_OR_GREATER || NETSTANDARD1_0_OR_GREATER || NET5_0_OR_GREATER)
        public static G GetAttribute<G>(this Type _this)
        {
            TypeInfo _ti = _this.GetTypeInfo();
            Attribute _a = _ti.GetCustomAttribute(typeof(G));

            if (_a == null)
                return default;

            if (_a is G _g)
                return _g;

            return default;
        }
#endif

    }
}
