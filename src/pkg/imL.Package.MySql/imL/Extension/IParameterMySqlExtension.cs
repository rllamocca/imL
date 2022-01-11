using imL.Contract.DB;
using imL.Utility;

using MySql.Data.MySqlClient;

using System.Collections.Generic;
using System.Linq;

namespace imL.Package.MySql
{
    public static class IParameterMySqlExtension
    {
        public static IEnumerable<MySqlParameter> GetMySqlParameters(this IEnumerable<IParameter> _array)
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
