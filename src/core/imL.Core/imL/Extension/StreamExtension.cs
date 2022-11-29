using System.IO;

namespace imL.Utility
{
    public static class StreamExtension
    {
        public static void CheckBeginPosition(this Stream _this)
        {
            if (_this == null)
                return;

            if (_this.Position > 0)
                _this.Seek(0, SeekOrigin.Begin);
        }

#if (NET35)
        public static void CopyTo(this Stream _this, Stream _to)
        {
            byte[] _buffer = new byte[128];
            int _read;

            while ((_read = _this.Read(_buffer, 0, _buffer.Length)) > 0)
                _to.Write(_buffer, 0, _read);
        }
#endif

#if (NET35_OR_GREATER || NETSTANDARD1_3_OR_GREATER || NET5_0_OR_GREATER)
        public static void FileCreate(this Stream _this, string _path)
        {
            using (FileStream _sw = File.Create(_path))
                _this.CopyTo(_sw);
        }
#endif

        public static byte[] ToBytes(this Stream _this)
        {
            using (MemoryStream _ms = new MemoryStream())
            {
                _this.CopyTo(_ms);

                return _ms.ToArray();
            }
        }

    }
}
