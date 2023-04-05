#if (NET35_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace imL
{
    public static partial class FtpHelper
    {
        internal static IEnumerable<FtpContentFormat> AnalizeListDirectoryDetailsISync(string _root, IEnumerable<string> _list)
        {
            foreach (string _item in _list)
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

        public static IEnumerable<string> ListDirectoryISync(string _root, FtpFormat _format)
        {
            FtpWebRequest _client = CreateClient(WebRequestMethods.Ftp.ListDirectory, _root, _format);

            using (FtpWebResponse _r = (FtpWebResponse)_client.GetResponse())
            using (StreamReader _sr = new StreamReader(_r.GetResponseStream()))
                while (_sr.EndOfStream == false)
                    yield return _sr.ReadLine();
        }
        public static IEnumerable<string> ListDirectoryDetailsISync(string _root, FtpFormat _format)
        {
            FtpWebRequest _client = CreateClient(WebRequestMethods.Ftp.ListDirectoryDetails, _root, _format);

            using (FtpWebResponse _r = (FtpWebResponse)_client.GetResponse())
            using (StreamReader _sr = new StreamReader(_r.GetResponseStream()))
                while (_sr.EndOfStream == false)
                    yield return _sr.ReadLine();
        }
        public static IEnumerable<FtpContentFormat> ListDirectoryDetailsContentISync(string _root, FtpFormat _format)
        {
            foreach (FtpContentFormat _item in AnalizeListDirectoryDetailsISync(_root, ListDirectoryDetailsISync(_root, _format)))
                yield return _item;
        }
        public static IEnumerable<FtpContentFormat> ListSubdirectoriesISync(string _root, FtpFormat _format)
        {
            foreach (FtpContentFormat _item in ListDirectoryDetailsContentISync(_root, _format))
            {
                yield return _item;

                if (_item.IsDirectory == true)
                    foreach (FtpContentFormat _item2 in ListSubdirectoriesISync(_root + "/" + _item.Name, _format))
                        yield return _item2;
            }
        }
    }
}

#endif