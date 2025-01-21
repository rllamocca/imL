using Microsoft.Data.SqlClient;

namespace imL.NC.pkg.MicrosoftEntityFrameworkCoreSqlServer
{
    public struct CommandInput
    {
        public EExecute? Execute { set; get; }
        public string Query { set; get; }
        public SqlParameter[]? Parameters { set; get; }

        public int? Timeout { set; get; }
        public SqlTransaction? Transaction { set; get; }

        public bool? EnforceConstraints { set; get; }
    }
}
