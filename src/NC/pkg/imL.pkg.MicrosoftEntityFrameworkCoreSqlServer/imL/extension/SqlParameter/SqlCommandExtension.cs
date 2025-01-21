using Microsoft.Data.SqlClient;

namespace imL.NC.pkg.MicrosoftEntityFrameworkCoreSqlServer
{
    public static partial class SqlCommandExtension
    {
        public static void OverwriteParameters(this SqlCommand? _this, SqlParameter[]? _reSet)
        {
            if (_this == null)
                return;

            if (_this.Parameters == null)
                return;

            if (_this.Parameters.Count < 1)
                return;

            _reSet = _reSet.OverwriteNULL();

            for (int _i = 0; _i < _this.Parameters.Count; _i++)
                _this.Parameters[_i].Value = _reSet[_i].Value;

            return;
        }
        
    }
}
