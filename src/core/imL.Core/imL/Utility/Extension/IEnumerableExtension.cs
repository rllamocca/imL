using System.Collections.Generic;
using System.Linq;

namespace imL.Utility
{
    public static class IEnumerableExtension
    {
        public static IEnumerable<T> DefaultOrEmpty<T>(this IEnumerable<T> _array)
        {
            return _array ?? Enumerable.Empty<T>();
        }
        public static bool HasValue<T>(this IEnumerable<T> _array)
        {
            return (_array != null);
        }
        public static bool IsEmpty<T>(this IEnumerable<T> _array)
        {
            return (_array == null || _array.Any() == false);
        }
        public static bool NotEmpty<T>(this IEnumerable<T> _array)
        {
            return (_array != null && _array.Any());
        }
    }
}