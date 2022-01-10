using imL.Contract.DB;

using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace imL.Utility.Sql
{
    public static class IParameterSqlExtension
    {
        public static IEnumerable<SqlParameter> GetSqlParameters(this IEnumerable<IParameter> _array)
        {
            if (_array.HasValue() == false)
                return null;

            return _array
                .Select(_s => (ParameterDefault)_s)
                .Where(_w => _w.Parameter != null)
                .Select(_s => _s.Parameter);
        }
    }
}
