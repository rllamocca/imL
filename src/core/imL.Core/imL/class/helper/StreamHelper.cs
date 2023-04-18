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
            switch (_compress)
            {
                case ECompress.Gzip:
                case ECompress.Deflate:
                    Stream _ms = new MemoryStream();
                    Stream _com;

                    switch (_compress)
                    {
                        case ECompress.Gzip:
                            _com = new GZipStream(_ms, CompressionMode.Compress, true);
                            _s.CopyTo(_com);
                            _com.Dispose();

                            break;
                        case ECompress.Deflate:
                            _com = new DeflateStream(_ms, CompressionMode.Compress, true);
                            _s.CopyTo(_com);
                            _com.Dispose();

                            break;
                        default:
                            break;
                    }

                    return _ms;
                default:
                    return _s;
            }
        }
        public static Stream Decompress(Stream _s, ECompress _compress = ECompress.Gzip)
        {
            switch (_compress)
            {
                case ECompress.Gzip:
                case ECompress.Deflate:
                    Stream _ms = new MemoryStream();
                    Stream _dec;

                    switch (_compress)
                    {
                        case ECompress.Gzip:
                            _dec = new GZipStream(_s, CompressionMode.Decompress, true);
                            _dec.CopyTo(_ms);
                            _dec.Dispose();

                            break;
                        case ECompress.Deflate:
                            _dec = new DeflateStream(_s, CompressionMode.Decompress, true);
                            _dec.CopyTo(_ms);
                            _dec.Dispose();

                            break;
                        default:
                            break;
                    }

                    return _ms;
                default:
                    return _s;
            }
        }
    }
}

#endif