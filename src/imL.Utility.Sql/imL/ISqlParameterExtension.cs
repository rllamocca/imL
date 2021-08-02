﻿using imL.Contract;
using imL.Utility.Sql.UserModel;

using System.Collections.Generic;
using System.Data.SqlClient;

namespace imL.Utility.Sql
{
    public static class ISqlParameterExtension
    {
        public static ISqlParameter[] GetSqlParameters(this IParameter[] _array)
        {
            if (_array == null)
                return null;

            List<ISqlParameter> _return = new List<ISqlParameter>();

            foreach (ISqlParameter _item in _array)
                _return.Add(_item);

            return _return.ToArray();
        }

        public static SqlParameter[] GetParameters(this ISqlParameter _this)
        {
            if (_this == null)
                return null;

            return new SqlParameter[] { _this.Parameter };
        }
        public static SqlParameter[] GetParameters(this ISqlParameter[] _array)
        {
            if (_array == null)
                return null;

            List<SqlParameter> _return = new List<SqlParameter>();
            foreach (ISqlParameter _item in _array)
                if (_item.Parameter != null)
                    _return.Add(_item.Parameter);

            return _return.ToArray();
        }
    }
}