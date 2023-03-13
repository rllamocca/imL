using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

using imL.DB;

namespace imL.Utility.Sql
{
    public static class IParameterSqlExtension
    {
        public static IEnumerable<SqlParameter> GetSqlParameters(this IEnumerable<IParameter> _array)
        {
            if (_array.HasValue() == false)
                return null;

            return _array
                .Where(_w => _w is SqlParameterDefault)
                .Select(_s => (SqlParameterDefault)_s)
                .Where(_w => _w.Parameter != null)
                .Select(_s => _s.Parameter);
        }
    }
}
