using System;

using Microsoft.Data.SqlClient;

namespace imL.NC.pkg.MicrosoftEntityFrameworkCoreSqlServer
{
    public static partial class SqlParameterExtension
    {
        public static SqlParameter[]? OverwriteNULL(this SqlParameter[]? _this)
        {
            if (_this == null)
                return null;

            if (_this.Length < 1)
                return null;

            foreach (SqlParameter _item in _this)
                _item.Value ??= DBNull.Value;

            return _this;
        }
        public static SqlParameter[]? Prepare(this SqlParameter[]? _this)
        {
            if (_this == null)
                return null;

            if (_this.Length < 1)
                return null;

            foreach (SqlParameter _item in _this)
                _item.Value = null;

            return _this;
        }
    }
}
