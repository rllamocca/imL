using System.Collections.Generic;
using System.Linq;

using imL.DB;

using Microsoft.Data.SqlClient;

namespace imL.Package.MicrosoftSql
{
    public static class IParameterSqlExtension
    {
        public static IEnumerable<SqlParameter> GetSqlParameters(this IEnumerable<IParameter> _array)
        {
            if (_array.HasValue() == false)
                return null;

            return _array
                .Where(_w => _w is MicrosoftSqlParameterDefault)
                .Select(_s => (MicrosoftSqlParameterDefault)_s)
                .Where(_w => _w.Parameter != null)
                .Select(_s => _s.Parameter);
        }
    }
}
