#if (NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER)

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace imL
{
    public static class IAsyncEnumerableExtension
    {
        public static async Task<IEnumerable<G>> ToAwaitIEnumerable<G>(this IAsyncEnumerable<G> _array)
        {
            IList<G> _return = new List<G>();

            await foreach (G _item in _array)
                _return.Add(_item);

            return _return;
        }
        public static async Task<G[]> ToAwaitArray<G>(this IAsyncEnumerable<G> _array)
        {
            IList<G> _return = new List<G>();

            await foreach (G _item in _array)
                _return.Add(_item);

            return _return.ToArray();
        }
    }
}

#endif