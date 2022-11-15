#if (NET40_OR_GREATER || NETSTANDARD1_0_OR_GREATER || NET5_0_OR_GREATER)

using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace imL.Utility
{
    public static class StreamAsyncExtension
    {

#if (NET45_OR_GREATER || NETSTANDARD1_3_OR_GREATER || NET5_0_OR_GREATER)
        public static async Task FileCreateAsync(this Stream _this, string _path, CancellationToken _token = default)
        {
            using (FileStream _sw = File.Create(_path, 128, FileOptions.Asynchronous))
                await _this.CopyToAsync(_sw, 128, _token);
        }
#endif

    }
}

#endif