using System;
using System.Collections.Generic;
using System.Linq;

using imL.DB;

namespace imL
{
    public static class SqlPattern
    {
        public static string Select(string _table, IParameter[] _array)
        {
            string _pattern = @"
SELECT
{1}
FROM [{0}]
{2};
";
            string _0 = _table;
            string _1 = null;
            string _2 = null;
            string[] _affects = _array.Where(_w => _w.Affect != null && _w.IsSearchCondition == false).Select(_s => _s.Affect).Distinct(StringComparer.OrdinalIgnoreCase).ToArray();
            IList<string> _tmp = _affects.Select(_s => string.Format("[{0}]", _s)).ToList();

            if (_tmp.Count == 0)
                _1 = "*";
            else
                _1 = string.Join(",", _tmp.ToArray());

            _affects = _array.Where(_w => _w.Affect != null && _w.IsSearchCondition == true).Select(_s => _s.Affect).Distinct(StringComparer.OrdinalIgnoreCase).ToArray();
            _tmp = new List<string>();

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
                _2 = string.Join(" AND ", _tmp.ToArray());
                _2 = string.Format("WHERE {0}", _2);
            }

            return string.Format(_pattern, _0, _1, _2);
        }
        public static string Insert(string _table, IParameter[] _array, bool _scope_identity = false)
        {
            string _pattern = @"
INSERT INTO [{0}]
({1})
VALUES
({2});{3}
";
            string _0 = _table;
            string _1 = null;
            string _2 = null;
            string _3 = null;
            string[] _affects = _array.Where(_w => _w.Affect != null).Select(_s => _s.Affect).Distinct(StringComparer.OrdinalIgnoreCase).ToArray();
            IList<string> _tmp = _affects.Select(_s => string.Format("[{0}]", _s)).ToList();
            _1 = string.Join(",", _tmp.ToArray());
            _tmp.Clear();

            foreach (string _item in _affects)
            {
                string[] _tmp2 = _array.Where(_w => _item.Equals(_w.Affect, StringComparison.OrdinalIgnoreCase) && _w.Expression != null).Select(_s => _s.Expression).ToArray();
                string _tmp3 = string.Join(" ", _tmp2.ToArray());

                if (_tmp2.Length > 1)
                    _tmp.Add(string.Format("({0})", _tmp3));
                else
                    _tmp.Add(_tmp3);
            }

            _2 = string.Join(",", _tmp.ToArray());

            if (_scope_identity)
                _3 = "SELECT SCOPE_IDENTITY();";

            return string.Format(_pattern, _0, _1, _2, _3);
        }
        public static string Update(string _table, IParameter[] _array)
        {
            string _pattern = @"
UPDATE [{0}] SET
{1}
{2};
";
            string _0 = _table;
            string _1 = null;
            string _2 = null;
            string[] _affects = _array.Where(_w => _w.Affect != null && _w.IsSearchCondition == false).Select(_s => _s.Affect).Distinct(StringComparer.OrdinalIgnoreCase).ToArray();
            IList<string> _tmp = new List<string>();

            foreach (string _item in _affects)
            {
                string[] _tmp2 = _array.Where(_w => _item.Equals(_w.Affect, StringComparison.OrdinalIgnoreCase) && _w.IsSearchCondition == false && _w.Expression != null).Select(_s => _s.Expression).ToArray();
                string _tmp3 = string.Join(" ", _tmp2.ToArray());

                if (_tmp2.Length > 1)
                    _tmp3 = string.Format("({0})", _tmp3);

                _tmp.Add(string.Format("[{0}] = {1}", _item, _tmp3));
            }

            _1 = string.Join(",", _tmp.ToArray());

            _affects = _array.Where(_w => _w.Affect != null && _w.IsSearchCondition == true).Select(_s => _s.Affect).Distinct(StringComparer.OrdinalIgnoreCase).ToArray();
            _tmp = new List<string>();

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
                _2 = string.Join(" AND ", _tmp.ToArray());
                _2 = string.Format("WHERE {0}", _2);
            }

            return string.Format(_pattern, _0, _1, _2);
        }
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
