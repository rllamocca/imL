﻿#if (NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER)

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;

namespace imL
{
    public static partial class FtpHelper
    {
        internal static async IAsyncEnumerable<FtpContentFormat> AnalizeListDirectoryDetailsIAsync(string _root, IAsyncEnumerable<string> _list, [EnumeratorCancellation] CancellationToken _ct = default)
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

#if NETSTANDARD2_1_OR_GREATER
                string _g = _item[54..];
#else
                string _g = _item.Substring(54, _item.Length - 54);
#endif

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

        public static async IAsyncEnumerable<string> ListDirectoryIAsync(string _root, FtpFormat _format, [EnumeratorCancellation] CancellationToken _ct = default)
        {
            FtpWebRequest _client = CreateClient(WebRequestMethods.Ftp.ListDirectory, _root, _format);

            using (FtpWebResponse _r = (FtpWebResponse)(await _client.GetResponseAsync()))
            using (StreamReader _sr = new StreamReader(_r.GetResponseStream()))
                while (_sr.EndOfStream == false)
#if NET7_0_OR_GREATER
                    yield return await _sr.ReadLineAsync(_ct);
#else
                    yield return await _sr.ReadLineAsync();
#endif

        }
        public static async IAsyncEnumerable<string> ListDirectoryDetailsIAsync(string _root, FtpFormat _format, [EnumeratorCancellation] CancellationToken _ct = default)
        {
            FtpWebRequest _client = CreateClient(WebRequestMethods.Ftp.ListDirectoryDetails, _root, _format);

            using (FtpWebResponse _r = (FtpWebResponse)(await _client.GetResponseAsync()))
            using (StreamReader _sr = new StreamReader(_r.GetResponseStream()))
                while (_sr.EndOfStream == false)
#if NET7_0_OR_GREATER
                    yield return await _sr.ReadLineAsync(_ct);
#else
                    yield return await _sr.ReadLineAsync();
#endif
        }
        public static async IAsyncEnumerable<FtpContentFormat> ListDirectoryDetailsContentIAsync(string _root, FtpFormat _format, [EnumeratorCancellation] CancellationToken _ct = default)
        {
            await foreach (FtpContentFormat _item in AnalizeListDirectoryDetailsIAsync(_root, ListDirectoryDetailsIAsync(_root, _format, _ct), _ct))
                yield return _item;
        }
        public static async IAsyncEnumerable<FtpContentFormat> ListSubdirectoriesIAsync(string _root, FtpFormat _format, [EnumeratorCancellation] CancellationToken _ct = default)
        {
            await foreach (FtpContentFormat _item in ListDirectoryDetailsContentIAsync(_root, _format, _ct))
            {
                yield return _item;

                if (_item.IsDirectory == true)
                    await foreach (FtpContentFormat _item2 in ListSubdirectoriesIAsync(_root + "/" + _item.Name, _format, _ct))
                        yield return _item2;
            }
        }
    }
}

#endif