using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

using imL.Utility.Ftp;

using Renci.SshNet;
using Renci.SshNet.Sftp;

namespace imL.Utility.SSH_Net
{
    public static class SftpHelper
    {
        private static void Init_SftpClient(ref SftpClient _ref, FtpFormat _format)
        {
            int _port = _format.Port ?? _ref.ConnectionInfo.Port;

            if (_port != _ref.ConnectionInfo.Port)
                _ref = new SftpClient(_format.Host, _port, _format.UserName, _format.Password);

            _ref.Connect();
        }
        private static SftpClient Create(FtpFormat _format)
        {
            SftpClient _return = new SftpClient(_format.Host, _format.UserName, _format.Password);
            SftpHelper.Init_SftpClient(ref _return, _format);

            return _return;
        }

        public static FtpStatusCode ListDirectory(out string[] _outList, FtpFormat _format)
        {
            SftpClient _client = SftpHelper.Create(_format);
            IEnumerable<SftpFile> _return = _client.ListDirectory(_format.Path);
            _client.Disconnect();
            _outList = _return.Select(_s => _s.Name).ToArray();

            return FtpStatusCode.CommandOK;
        }

        public static FtpStatusCode DownloadFile(ref Stream _refDown, FtpFormat _format)
        {
            SftpClient _client = SftpHelper.Create(_format);
            _client.DownloadFile(_format.Path, _refDown);
            _client.Disconnect();

            return FtpStatusCode.CommandOK;
        }

        public static FtpStatusCode UploadFile(Stream _up, FtpFormat _format)
        {
            SftpClient _client = SftpHelper.Create(_format);
            _client.UploadFile(_up, _format.Path);
            _client.Disconnect();

            return FtpStatusCode.CommandOK;
        }

        public static FtpStatusCode DeleteFile(FtpFormat _format)
        {
            SftpClient _client = SftpHelper.Create(_format);
            _client.Delete(_format.Path);
            _client.Disconnect();

            return FtpStatusCode.CommandOK;
        }
    }
}
