using System;
using System.IO;

using imL.Format;
using imL.Utility;

namespace SAMPLE.FTP.imL.Core
{
    internal partial class Program
    {
        static void Test_ListDirectory(FtpFormat _format)
        {
            var _return = FtpHelper.ListDirectory(null, _format);

            if (_return == null)
                throw new ArgumentNullException(nameof(_return));

            Console.WriteLine(_format.Host);

            foreach (var _item in _return)
                Console.WriteLine(_item);
        }
        static void Test_ListSubdirectories(FtpFormat _format)
        {
            var _return = FtpHelper.ListSubdirectories(null, _format);

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

                    using (var _down = FtpHelper.DownloadFile(_item.FullName, _format))
                    {
                        Console.WriteLine("{0}", _item.Name);

                        using (var _fs = new FileStream(_item.Name, FileMode.Create))
                            _down.CopyTo(_fs);
                    }
                }
            }
        }
    }
}
