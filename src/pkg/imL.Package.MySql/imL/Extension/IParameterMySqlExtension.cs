using System.Collections.Generic;
using System.Linq;

using imL.DB;

using MySql.Data.MySqlClient;

namespace imL.Package.MySql
{
    public static class IParameterMySqlExtension
    {
        public static IEnumerable<MySqlParameter> GetMySqlParameters(this IEnumerable<IParameter> _array)
        {
            if (_array.HasValue() == false)
                return null;

            return _array
                .Where(_w => _w is MySqlParameterDefault)
                .Select(_s => (MySqlParameterDefault)_s)
                .Where(_w => _w.Parameter != null)
                .Select(_s => _s.Parameter);
        }
    }
}
