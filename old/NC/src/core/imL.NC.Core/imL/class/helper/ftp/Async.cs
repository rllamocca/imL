#if (NET45_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)

#if (NET6_0_OR_GREATER)
#endif

using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace imL
{
    public static partial class FtpHelper
    {
        public static async Task<IEnumerable<string>> ListDirectoryAsync(string _root, FtpFormat _format, CancellationToken _ct = default)
        {
            //return (await ListDirectoryIAsync(_root, _format).ToIEnumerable()).ToArray();
            return await ListDirectoryIAsync(_root, _format, _ct).ToIEnumerable();
        }
        public static async Task<IEnumerable<string>> ListDirectoryDetailsAsync(string _root, FtpFormat _format, CancellationToken _ct = default)
        {
            return await ListDirectoryDetailsIAsync(_root, _format, _ct).ToIEnumerable();
        }
        public static async Task<IEnumerable<FtpContentFormat>> ListDirectoryDetailsContentAsync(string _root, FtpFormat _format, CancellationToken _ct = default)
        {
            return await AnalizeListDirectoryDetailsIAsync(_root, ListDirectoryDetailsIAsync(_root, _format, _ct), _ct).ToIEnumerable();
        }
        public static async Task<IEnumerable<FtpContentFormat>> ListSubdirectoriesAsync(string _root, FtpFormat _format, CancellationToken _ct = default)
        {
            return await ListSubdirectoriesIAsync(_root, _format, _ct).ToIEnumerable();
        }

        public static async Task<FtpStatusCode> UploadFileAsync(string _root, Stream _up, FtpFormat _format, CancellationToken _ct = default)
        {
            FtpWebRequest _client = CreateClient(WebRequestMethods.Ftp.UploadFile, _root, _format);

            using (Stream _s = _client.GetRequestStream())
            {
                await _up.CopyToAsync(_s, _ct);

                using (FtpWebResponse _resp = (FtpWebResponse)(await _client.GetResponseAsync()))
                    return _resp.StatusCode;
            }
        }
        public static async Task<FtpStatusCode> UploadFileAsync(string _root, byte[] _up, FtpFormat _format, CancellationToken _ct = default)
        {
            using (Stream _ms = new MemoryStream(_up))
                return await UploadFileAsync(_root, _ms, _format, _ct);
        }
        public static async Task<Stream> DownloadFileAsync(string _root, FtpFormat _format, CancellationToken _ct = default)
        {
            Stream _return = new MemoryStream();
            FtpWebRequest _client = CreateClient(WebRequestMethods.Ftp.DownloadFile, _root, _format);

            using (FtpWebResponse _r = (FtpWebResponse)(await _client.GetResponseAsync()))
            {
                using (Stream _s = _r.GetResponseStream())
                    await _s.CopyToAsync(_return, _ct);

                _return.CheckBeginPosition();

                return _return;
            }
        }
        public static async Task<byte[]> DownloadFileBytesAsync(string _root, FtpFormat _format, CancellationToken _ct = default)
        {
            using (Stream _s = await DownloadFileAsync(_root, _format, _ct))
                return _s.ToBytes();
        }
        public static async Task<FtpStatusCode> DeleteFileAsync(string _root, FtpFormat _format, CancellationToken _ct = default)
        {
            FtpWebRequest _client = CreateClient(WebRequestMethods.Ftp.DeleteFile, _root, _format);

            using (FtpWebResponse _resp = (FtpWebResponse)(await _client.GetResponseAsync()))
                return _resp.StatusCode;
        }
    }
}

#endif