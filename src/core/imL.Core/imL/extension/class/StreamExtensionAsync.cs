#if (NET40_OR_GREATER || NETSTANDARD1_0_OR_GREATER || NET5_0_OR_GREATER)

using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace imL
{
    public static class StreamExtensionAsync
    {

#if (NET45_OR_GREATER || NETSTANDARD1_3_OR_GREATER || NET5_0_OR_GREATER)
        public static async Task FileCreateAsync(this Stream _this, string _path, CancellationToken _token = default)
        {
            int _buffer = 128;

            using (FileStream _sw = File.Create(_path, _buffer, FileOptions.Asynchronous))
                await _this.CopyToAsync(_sw, _buffer, _token);
        }
#endif

#if (NET40 == false)
        public static byte[] ToBytesAsync(this Stream _this)
        {
            using (MemoryStream _ms = new MemoryStream())
            {
                _this.CopyToAsync(_ms);

                return _ms.ToArray();
            }
        }
#endif

    }
}

#endif