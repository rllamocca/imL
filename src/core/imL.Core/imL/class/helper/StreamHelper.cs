#if (NET35_OR_GREATER || NETSTANDARD1_1_OR_GREATER || NET5_0_OR_GREATER)

using System.IO;
using System.IO.Compression;

using imL.Http;

namespace imL
{
    public static class StreamHelper
    {
        public static Stream Compress(Stream _s, ECompress _compress = ECompress.Gzip)
        {
            Stream _ms = new MemoryStream();
            Stream _com = null;

            switch (_compress)
            {
                case ECompress.Gzip:
                    _com = new GZipStream(_ms, CompressionMode.Compress, true);
                    break;
                case ECompress.Deflate:
                    _com = new DeflateStream(_ms, CompressionMode.Compress, true);
                    break;
                default:
                    break;
            }

            switch (_compress)
            {
                case ECompress.Gzip:
                case ECompress.Deflate:
                    _s.CopyTo(_com);
                    _com.Dispose();

                    break;
                default:
                    return _s;
            }

            return _ms;
        }
        public static Stream Decompress(Stream _s, ECompress _compress = ECompress.Gzip)
        {
            Stream _ms = new MemoryStream();
            Stream _dec = null;

            switch (_compress)
            {
                case ECompress.Gzip:
                    _dec = new GZipStream(_s, CompressionMode.Decompress, true);

                    break;
                case ECompress.Deflate:
                    _dec = new DeflateStream(_s, CompressionMode.Decompress, true);

                    break;
                default:
                    break;
            }

            switch (_compress)
            {
                case ECompress.Gzip:
                case ECompress.Deflate:
                    _dec.CopyTo(_ms);
                    _dec.Dispose();

                    break;
                default:

                    return _s;
            }

            return _ms;
        }
    }
}

#endif