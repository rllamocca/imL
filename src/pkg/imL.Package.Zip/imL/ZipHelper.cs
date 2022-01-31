#if (NET35 || NET40)
using ICSharpCode.SharpZipLib.Zip;

using imL.Utility;
#else
using System.IO.Compression;
#endif

using System;
using System.IO;
using System.Linq;
using imL.Struct;

namespace imL.Package.Zip
{
    public class ZipHelper
    {
        public static string Compress(string _from, string _to = null)
        {
            FileInfo _info = new FileInfo(_from);

            if (_info.Exists == false)
                throw new FileNotFoundException(nameof(_from), _from);

            if (_info.Extension.ToLower() == ".zip")
                return _from;

            if (_to == null)
                _to = Path.ChangeExtension(_info.FullName, ".zip");

            using (FileStream _fs = new FileStream(_to, FileMode.Create))
            {
#if (NET35 || NET40)
                using (ZipOutputStream _zos = new ZipOutputStream(_fs))
                {
                    _zos.SetLevel(9);

                    ZipEntry _ze = new ZipEntry(_info.Name)
                    {
                        DateTime = DateTime.UtcNow
                    };
                    _zos.PutNextEntry(_ze);
                    _info.OpenRead().CopyTo(_zos);
                    _zos.CloseEntry();
                    _zos.IsStreamOwner = true;
                    _zos.Close();
                }
#else
                using (ZipArchive _za = new ZipArchive(_fs, ZipArchiveMode.Create))
                {
                    _za.CreateEntryFromFile(_info.FullName, _info.Name, CompressionLevel.Optimal);
                }
#endif
            }

            return _to;
        }
        public static string CompressOnly(string _from, MemoryUnit _min, params string[] _exts)
        {
            FileInfo _info = new FileInfo(_from);

            if (_info.Exists == false)
                throw new FileNotFoundException(nameof(_from), _from);

            if (_exts.Contains(_info.Extension.ToLower()) == false)
                return _from;

            if (_min < new MemoryUnit(_info.Length))
                return ZipHelper.Compress(_from);

            return _from;
        }
    }
}
