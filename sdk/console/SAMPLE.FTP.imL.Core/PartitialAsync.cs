﻿#if  (NET45_OR_GREATER || NET5_0_OR_GREATER)

using System;
using System.IO;
using System.Threading.Tasks;

using imL;

namespace SAMPLE.FTP.imL.Core
{
    internal partial class Program
    {
        static async Task Test_ListDirectoryAsync(FtpFormat _format)
        {
            var _return = await FtpHelper.ListDirectoryAsync(null, _format);

            if (_return == null)
                throw new ArgumentNullException(nameof(_return));

            Console.WriteLine(_format.Host);

            foreach (var _item in _return)
                Console.WriteLine(_item);
        }
        static async Task Test_ListSubdirectoriesAsync(FtpFormat _format)
        {
            var _return = await FtpHelper.ListSubdirectoriesAsync(null, _format);

            if (_return == null)
                throw new ArgumentNullException(nameof(_return));

            Console.WriteLine(_format.Host);
            bool _onefile = false;

            foreach (var _item in _return)
            {
                Console.WriteLine("{0} {1} {2} {3}", _item.FullName, _item.Name, _item.Size, _item.IsDirectory);

                if (_item.IsDirectory == false && _onefile == false)
                {
                    _onefile = true;

                    using (var _down = await FtpHelper.DownloadFileAsync(_item.FullName, _format))
                    {
                        Console.WriteLine("{0}", _item.Name);

                        using (var _fs = new FileStream(_item.Name, FileMode.Create))
                            await _down.CopyToAsync(_fs);
                    }
                }
            }
        }
    }
}

#endif
