using System.Collections.Generic;

using imL.Contract.DB;
using imL.Utility.MySql.Fulfill;

using MySql.Data.MySqlClient;

namespace imL.Utility.MySql
{
    public static class IParameter_imLUtilityMySqlExtension
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

        public static MySqlParameter[] GetMySqlParameters(this FParameter[] _array)
        {
            if (_array == null)
                return null;

            List<MySqlParameter> _return = new List<MySqlParameter>();

            foreach (FParameter _item in _array)
                if (_item.Parameter != null)
                    _return.Add(_item.Parameter);

            return _return.ToArray();
        }
    }
}
