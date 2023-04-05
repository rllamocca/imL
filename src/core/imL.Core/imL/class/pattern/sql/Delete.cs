using System;
using System.Collections.Generic;
using System.Linq;

using imL.DB;

namespace imL
{
    public static partial class SqlPattern
    {
        public static string Delete(string _table, IParameter[] _array)
        {
            string _pattern = @"
DELETE FROM [{0}]
{1};
";
            string _0 = _table;
            string _1 = null;
            string[] _affects = _array.Where(_w => _w.Affect != null && _w.IsSearchCondition == true).Select(_s => _s.Affect).Distinct(StringComparer.OrdinalIgnoreCase).ToArray();
            IList<string> _tmp = new List<string>();

            foreach (string _item in _affects)
            {
                string[] _tmp2 = _array.Where(_w => _item.Equals(_w.Affect, StringComparison.OrdinalIgnoreCase) && _w.IsSearchCondition == true && _w.Expression != null).Select(_s => _s.Expression).ToArray();
                string _tmp3 = string.Join(" ", _tmp2.ToArray());

                if (_tmp2.Length > 1)
                    _tmp3 = string.Format("({0})", _tmp3);

                _tmp.Add(string.Format("[{0}] = {1}", _item, _tmp3));
            }

            if (_tmp.Count > 0)
            {
                _1 = string.Join(" AND ", _tmp.ToArray());
                _1 = string.Format("WHERE {0}", _1);
            }

            return string.Format(_pattern, _0, _1);
        }
    }
}
