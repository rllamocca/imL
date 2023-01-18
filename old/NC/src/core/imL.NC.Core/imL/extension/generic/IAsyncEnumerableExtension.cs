using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace imL
{
    public static class IAsyncEnumerableExtension
    {
        public static async Task<IEnumerable<T>> ToIEnumerable<T>(this IAsyncEnumerable<T> _array)
        {
            if (_array == null)
                throw new ArgumentNullException(nameof(_array));

            IList<T> _return = new List<T>();

            await foreach (T _item in _array)
                _return.Add(_item);

            return _return;
        }
    }
}
