#if (NET35 || NET40)
using System;

using ICSharpCode.SharpZipLib.Zip;

using imL.Utility;
#else
using System.IO.Compression;
#endif

using System.IO;

namespace imL.Package.Zip
{
    public class ZipHelper
    {
        public static string Compress(string _from, string _to = null)
        {
            FileInfo _fi = new FileInfo(_from);

            if (_fi.Exists == false)
                throw new FileNotFoundException(nameof(_from), _from);

            if (_to == null)
                _to = Path.ChangeExtension(_fi.FullName, ".zip");

            using (FileStream _fs = new FileStream(_to, FileMode.Create))
            {
#if (NET35 || NET40)
                using (ZipOutputStream _zos = new ZipOutputStream(_fs))
                {
                    _zos.SetLevel(9);

                    ZipEntry _ze = new ZipEntry(_fi.Name)
                    {
                        DateTime = DateTime.UtcNow
                    };
                    _zos.PutNextEntry(_ze);
                    _fi.OpenRead().CopyTo(_zos);
                    _zos.CloseEntry();
                    _zos.IsStreamOwner = true;
                    _zos.Close();
                }
#else
                using (ZipArchive _za = new ZipArchive(_fs, ZipArchiveMode.Create))
                {
                    _za.CreateEntryFromFile(_fi.FullName, _fi.Name, CompressionLevel.Optimal);
                }
#endif
            }

            return _to;
        }
    }
}
