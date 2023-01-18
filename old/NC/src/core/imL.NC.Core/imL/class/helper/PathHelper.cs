#if (NET35)

using System;
using System.IO;

namespace imL
{
    public static class PathHelper
    {
        public static string Combine(params string[] _paths)
        {
            if (_paths == null)
                new ArgumentNullException(nameof(_paths));

            if (_paths.Length > 0)
            {
                string _return = Path.Combine("", _paths[0]);

                for (int _i = 1; _i < _paths.Length; _i++)
                    _return = Path.Combine(_return, _paths[_i]);

                return  _return;
            }

            throw new ArgumentNullException(nameof(_paths));
        }
    }
}

#endif