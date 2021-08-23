using System.Collections.Generic;

using imL.Contract.DB;
using imL.Utility.MySql.UserModel;

using MySql.Data.MySqlClient;

namespace imL.Utility.MySql
{
    public static class IParameter_imLUtilityMySqlExtension
    {
        public static IMySqlParameter[] GetParameters(this IParameter[] _array)
        {
            if (_array == null)
                return null;

            List<IMySqlParameter> _return = new List<IMySqlParameter>();

            foreach (IMySqlParameter _item in _array)
                _return.Add(_item);

            return _return.ToArray();
        }

        public static MySqlParameter[] GetMySqlParameters(this IMySqlParameter[] _array)
        {
            if (_array == null)
                return null;

            List<MySqlParameter> _return = new List<MySqlParameter>();

            foreach (IMySqlParameter _item in _array)
                if (_item.Parameter != null)
                    _return.Add(_item.Parameter);

            return _return.ToArray();
        }
    }
}
