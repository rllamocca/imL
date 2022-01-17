using imL.Contract.DB;
using imL.Utility;

using Microsoft.Data.SqlClient;

namespace imL.Package.EFCSql
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
