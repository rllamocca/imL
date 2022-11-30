#if (NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER)

#if (NET6_0_OR_GREATER)
using System;
#endif

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

using imL.Format;

namespace imL.Utility
{
#if (NET6_0_OR_GREATER)
    [Obsolete("https://docs.microsoft.com/en-us/dotnet/fundamentals/syslib-diagnostics/syslib0014")]
#endif
    public static class FtpHelperIAsync
    {
        internal static async IAsyncEnumerable<FtpContentFormat> AnalizeListDirectoryDetailsIAsync(string _root, IAsyncEnumerable<string> _list)
        {
            await foreach (string _item in _list)
            {
                //drwxrwxrwx   1 user     group           0 Nov 21 22:05 221117
                //-rw-rw-rw-   1 user     group     7206316 Nov 21 15:08 20221121_7849166.zip

                //string _a = _item.Substring(0, 10);
                //string _b = _item.Substring(10, 4);
                //string _c = _item.Substring(14, 5);
                //string _d = _item.Substring(19, 10);
                string _e = _item.Substring(29, 12);
                //string _f = _item.Substring(41, 13);
                string _g = _item.Substring(54, _item.Length - 54);

                _e = _e.Trim();
                _g = _g.Trim();

                string _name = _g;

                if (_name == "." || _name == "..")
                    continue;

                long _size = Convert.ToInt64(_e);

                yield return new FtpContentFormat()
                {
                    IsDirectory = _size < 1,
                    Size = _size,
                    Name = _name,
                    FullName = _root + "/" + _name
                };
            }

        }

        public static async IAsyncEnumerable<string> ListDirectoryIAsync(string _root, FtpFormat _format)
        {
            FtpWebRequest _client = FtpHelper.CreateClient(WebRequestMethods.Ftp.ListDirectory, _root, _format);

            using (FtpWebResponse _r = (FtpWebResponse)(await _client.GetResponseAsync()))
            using (StreamReader _sr = new StreamReader(_r.GetResponseStream()))
                while (_sr.EndOfStream == false)
                    yield return await _sr.ReadLineAsync();
        }
        public static async IAsyncEnumerable<string> ListDirectoryDetailsIAsync(string _root, FtpFormat _format)
        {
            FtpWebRequest _client = FtpHelper.CreateClient(WebRequestMethods.Ftp.ListDirectoryDetails, _root, _format);

            using (FtpWebResponse _r = (FtpWebResponse)(await _client.GetResponseAsync()))
            using (StreamReader _sr = new StreamReader(_r.GetResponseStream()))
                while (_sr.EndOfStream == false)
                    yield return await _sr.ReadLineAsync();
        }
        public static async IAsyncEnumerable<FtpContentFormat> ListDirectoryDetailsContentIAsync(string _root, FtpFormat _format)
        {
            await foreach (FtpContentFormat _item in AnalizeListDirectoryDetailsIAsync(_root, ListDirectoryDetailsIAsync(_root, _format)))
                yield return _item;
        }
        public static async IAsyncEnumerable<FtpContentFormat> ListSubdirectoriesIAsync(string _root, FtpFormat _format)
        {
            await foreach (FtpContentFormat _item in ListDirectoryDetailsContentIAsync(_root, _format))
            {
                yield return _item;

                if (_item.IsDirectory == true)
                    await foreach (FtpContentFormat _item2 in ListSubdirectoriesIAsync(_root + "/" + _item.Name, _format))
                        yield return _item2;
            }
        }
    }
}

#endif