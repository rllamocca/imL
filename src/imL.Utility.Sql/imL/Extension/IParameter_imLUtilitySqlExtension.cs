using System.Collections.Generic;
using System.Data.SqlClient;

using imL.Contract.DB;
using imL.Utility.Sql.Fulfill;

namespace imL.Utility.Sql
{
    public static class IParameter_imLUtilitySqlExtension
    {
        public static FParameter[] GetParameters(this IParameter[] _array)
        {
            if (_array == null)
                return null;

            List<FParameter> _return = new List<FParameter>();

            foreach (FParameter _item in _array)
                _return.Add(_item);

            return _return.ToArray();
        }

        public static SqlParameter[] GetSqlParameters(this FParameter[] _array)
        {
            if (_array == null)
                return null;

            List<SqlParameter> _return = new List<SqlParameter>();

            foreach (FParameter _item in _array)
                if (_item.Parameter != null)
                    _return.Add(_item.Parameter);

            return _return.ToArray();
        }
    }
}
