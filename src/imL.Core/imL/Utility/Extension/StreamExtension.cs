#if (NET35 || NET40) == false
using System.Threading;
using System.Threading.Tasks;
#endif

using System.IO;

namespace imL.Utility
{
    public static class StreamExtension
    {
        public static void OldCopyTo(this Stream _this, Stream _to)
        {
            byte[] _buffer = new byte[128];
            int _read;

            while ((_read = _this.Read(_buffer, 0, _buffer.Length)) > 0)
                _to.Write(_buffer, 0, _read);
        }

#if (NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2) == false

        public static void FileCreate(this Stream _this, string _path)
        {
            _this.Seek(0, SeekOrigin.Begin);

            using (FileStream _sw = File.Create(_path))
            {

#if (NET35)
                _this.OldCopyTo(_sw);
#else    
                _this.CopyTo(_sw);
#endif

            }
        }

#if (NET35 || NET40) == false

        public async static Task FileCreateAsync(this Stream _this, string _path, CancellationToken _token = default)
        {
            _this.Seek(0, SeekOrigin.Begin);

            using (FileStream _sw = File.Create(_path, 128, FileOptions.Asynchronous))
                await _this.CopyToAsync(_sw, 128, _token);
        }

#endif

#endif

    }
}
