#if (NET35_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace imL
{
    public static partial class FtpHelper
    {
        public static IEnumerable<string> ListDirectory(string _root, FtpFormat _format)
        {
            return ListDirectoryISync(_root, _format).ToArray();
        }
        public static IEnumerable<string> ListDirectoryDetails(string _root, FtpFormat _format)
        {
            return ListDirectoryDetailsISync(_root, _format).ToArray();
        }
        public static IEnumerable<FtpContentFormat> ListDirectoryDetailsContent(string _root, FtpFormat _format)
        {
            return AnalizeListDirectoryDetailsISync(_root, ListDirectoryDetailsISync(_root, _format)).ToArray();
        }
        public static IEnumerable<FtpContentFormat> ListSubdirectories(string _root, FtpFormat _format)
        {
            return ListSubdirectoriesISync(_root, _format).ToArray();
        }

        public static FtpStatusCode UploadFile(string _root, Stream _up, FtpFormat _format)
        {
            FtpWebRequest _client = CreateClient(WebRequestMethods.Ftp.UploadFile, _root, _format);

            using (Stream _s = _client.GetRequestStream())
            {
                _up.CopyTo(_s);

                using (FtpWebResponse _resp = (FtpWebResponse)_client.GetResponse())
                    return _resp.StatusCode;
            }
        }
        public static FtpStatusCode UploadFile(string _root, byte[] _up, FtpFormat _format)
        {
            using (Stream _ms = new MemoryStream(_up))
                return UploadFile(_root, _ms, _format);
        }
        public static Stream DownloadFile(string _root, FtpFormat _format)
        {
            FtpWebRequest _client = CreateClient(WebRequestMethods.Ftp.DownloadFile, _root, _format);
            Stream _return = new MemoryStream();

            using (FtpWebResponse _r = (FtpWebResponse)_client.GetResponse())
            {
                using (Stream _s = _r.GetResponseStream())
                    _s.CopyTo(_return);

                _return.CheckBeginPosition();

                return _return;
            }
        }
        public static byte[] DownloadFileBytes(string _root, FtpFormat _format)
        {
            using (Stream _s = DownloadFile(_root, _format))
                return _s.ToBytes();
        }
        public static FtpStatusCode DeleteFile(string _root, FtpFormat _format)
        {
            FtpWebRequest _client = CreateClient(WebRequestMethods.Ftp.DeleteFile, _root, _format);

            using (FtpWebResponse _resp = (FtpWebResponse)_client.GetResponse())
                return _resp.StatusCode;
        }
    }
}

#endif