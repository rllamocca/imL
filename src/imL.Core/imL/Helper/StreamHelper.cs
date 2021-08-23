﻿#if (NETSTANDARD1_0  == false)

using System.IO;
using System.IO.Compression;

using imL.Enumeration.Http;
using imL.Utility;

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
#if (NET35)
                    _s.OldCopyTo(_com);
#else
                    _s.CopyTo(_com);
#endif
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
#if (NET35)
                    _dec.OldCopyTo(_ms);
#else
                    _dec.CopyTo(_ms);
#endif
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