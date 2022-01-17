#if (NET35_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)

#if (NET6_0_OR_GREATER)
using System;
#endif

using System.Collections.Generic;
using System.IO;
using System.Net;

using imL.Format;

namespace imL.Utility
{
#if (NET6_0_OR_GREATER)
    [Obsolete("https://docs.microsoft.com/en-us/dotnet/fundamentals/syslib-diagnostics/syslib0014")]
#endif
    public static class FtpHelper
    {
        internal static void Init_FtpWebRequest(ref FtpWebRequest _ref, FtpFormat _format)
        {
            _ref.UseBinary = _format.UseBinary ?? _ref.UseBinary;
            _ref.Timeout = _format.Timeout ?? _ref.Timeout;
            _ref.ReadWriteTimeout = _format.ReadWriteTimeout ?? _ref.ReadWriteTimeout;
            _ref.KeepAlive = _format.KeepAlive ?? _ref.KeepAlive;
            _ref.EnableSsl = _format.EnableSsl ?? _ref.EnableSsl;
            _ref.UsePassive = _format.UsePassive ?? _ref.UsePassive;

            _ref.UseDefaultCredentials = _format.UseDefaultCredentials ?? _ref.UseDefaultCredentials;

            if (_ref.UseDefaultCredentials == false)
                _ref.Credentials = new NetworkCredential(_format.UserName, _format.Password);
        }
        internal static FtpWebRequest Create(FtpFormat _format, string _method)
        {
            FtpWebRequest _return = (FtpWebRequest)FtpWebRequest.Create(_format.Host + _format.Path);
            FtpHelper.Init_FtpWebRequest(ref _return, _format);
            _return.Method = _method;

            return _return;
        }

        public static FtpStatusCode ListDirectory(out string[] _outList, FtpFormat _format)
        {
            FtpWebRequest _client = FtpHelper.Create(_format, WebRequestMethods.Ftp.ListDirectory);
            List<string> _return = new List<string>();

            using (FtpWebResponse _resp = (FtpWebResponse)_client.GetResponse())
            {
                using (StreamReader _sr = new StreamReader(_resp.GetResponseStream()))
                    while (_sr.EndOfStream == false)
                        _return.Add(_sr.ReadLine());

                _outList = _return.ToArray();

                return _resp.StatusCode;
            }
        }

        public static FtpStatusCode DownloadFile(ref Stream _refDown, FtpFormat _format)
        {
            FtpWebRequest _client = FtpHelper.Create(_format, WebRequestMethods.Ftp.DownloadFile);

            using (FtpWebResponse _resp = (FtpWebResponse)_client.GetResponse())
            {
                using (StreamReader _sr = new StreamReader(_resp.GetResponseStream()))
                {
                    StreamWriter _sw = new StreamWriter(_refDown);
                    _sw.Write(_sr.ReadToEnd());
                    _sw.Flush();
                }

                return _resp.StatusCode;
            }
        }

        public static FtpStatusCode UploadFile(Stream _up, FtpFormat _format)
        {
            FtpWebRequest _client = FtpHelper.Create(_format, WebRequestMethods.Ftp.UploadFile);

            using (Stream _s = _client.GetRequestStream())
            {
                _up.CopyTo(_s);
            }

            using (FtpWebResponse _resp = (FtpWebResponse)_client.GetResponse())
                return _resp.StatusCode;
        }

        public static FtpStatusCode DeleteFile(FtpFormat _format)
        {
            FtpWebRequest _client = FtpHelper.Create(_format, WebRequestMethods.Ftp.DeleteFile);

            using (FtpWebResponse _resp = (FtpWebResponse)_client.GetResponse())
                return _resp.StatusCode;
        }
    }
}

#endif