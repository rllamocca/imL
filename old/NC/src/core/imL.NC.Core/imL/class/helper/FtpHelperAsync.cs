#if (NET45_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)

#if (NET6_0_OR_GREATER)
#endif

using System.Net;

namespace imL
{
#if (NET6_0_OR_GREATER)
    [Obsolete("https://docs.microsoft.com/en-us/dotnet/fundamentals/syslib-diagnostics/syslib0014")]
#endif
    public static class FtpHelperAsync
    {
        public static async Task<IEnumerable<string>> ListDirectoryAsync(string _root, FtpFormat _format)
        {
            IList<string> _return = new List<string>();
            FtpWebRequest _client = FtpHelper.CreateClient(WebRequestMethods.Ftp.ListDirectory, _root, _format);

            using (FtpWebResponse _r = (FtpWebResponse)(await _client.GetResponseAsync()))
            using (StreamReader _sr = new StreamReader(_r.GetResponseStream()))
                while (_sr.EndOfStream == false)
                    _return.Add(await _sr.ReadLineAsync());

            return _return;
        }
        public static async Task<IEnumerable<string>> ListDirectoryDetailsAsync(string _root, FtpFormat _format)
        {
            IList<string> _return = new List<string>();
            FtpWebRequest _client = FtpHelper.CreateClient(WebRequestMethods.Ftp.ListDirectoryDetails, _root, _format);

            using (FtpWebResponse _r = (FtpWebResponse)(await _client.GetResponseAsync()))
            using (StreamReader _sr = new StreamReader(_r.GetResponseStream()))
                while (_sr.EndOfStream == false)
                    _return.Add(await _sr.ReadLineAsync());

            return _return;
        }
        public static async Task<IEnumerable<FtpContentFormat>> ListDirectoryDetailsContentAsync(string _root, FtpFormat _format)
        {
            return FtpHelper.AnalizeListDirectoryDetails(_root, await ListDirectoryDetailsAsync(_root, _format));
        }
        public static async Task<IEnumerable<FtpContentFormat>> ListSubdirectoriesAsync(string _root, FtpFormat _format)
        {
            List<FtpContentFormat> _return = new List<FtpContentFormat>();
            IEnumerable<FtpContentFormat> _tmp = await ListDirectoryDetailsContentAsync(_root, _format);
            _return.AddRange(_tmp);

            foreach (FtpContentFormat _item in _tmp)
                if (_item.IsDirectory == true)
                    _return.AddRange(await ListSubdirectoriesAsync(_root + "/" + _item.Name, _format));

            return _return;
        }

        public static async Task<FtpStatusCode> UploadFileAsync(string _root, Stream _up, FtpFormat _format)
        {
            FtpWebRequest _client = FtpHelper.CreateClient(WebRequestMethods.Ftp.UploadFile, _root, _format);

            using (Stream _s = _client.GetRequestStream())
            {
                await _up.CopyToAsync(_s);

                using (FtpWebResponse _resp = (FtpWebResponse)(await _client.GetResponseAsync()))
                    return _resp.StatusCode;
            }
        }
        public static async Task<FtpStatusCode> UploadFileAsync(string _root, byte[] _up, FtpFormat _format)
        {
            using (Stream _ms = new MemoryStream(_up))
                return await UploadFileAsync(_root, _ms, _format);
        }
        public static async Task<Stream> DownloadFileAsync(string _root, FtpFormat _format)
        {
            Stream _return = new MemoryStream();
            FtpWebRequest _client = FtpHelper.CreateClient(WebRequestMethods.Ftp.DownloadFile, _root, _format);

            using (FtpWebResponse _r = (FtpWebResponse)(await _client.GetResponseAsync()))
            {
                using (Stream _s = _r.GetResponseStream())
                    await _s.CopyToAsync(_return);

                _return.CheckBeginPosition();

                return _return;
            }
        }
        public static async Task<byte[]> DownloadFileBytesAsync(string _root, FtpFormat _format)
        {
            using (Stream _s = await DownloadFileAsync(_root, _format))
                return _s.ToBytes();
        }
        public static async Task<FtpStatusCode> DeleteFileAsync(string _root, FtpFormat _format)
        {
            FtpWebRequest _client = FtpHelper.CreateClient(WebRequestMethods.Ftp.DeleteFile, _root, _format);

            using (FtpWebResponse _resp = (FtpWebResponse)(await _client.GetResponseAsync()))
                return _resp.StatusCode;
        }
    }
}

#endif