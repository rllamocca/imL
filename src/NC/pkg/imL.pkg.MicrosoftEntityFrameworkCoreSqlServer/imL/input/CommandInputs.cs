using System.Linq;

using Microsoft.Data.SqlClient;

namespace imL.NC.pkg.MicrosoftEntityFrameworkCoreSqlServer
{
    public struct CommandInputs
    {
        public EExecute? Execute { set; get; }
        public string Query { set; get; }
        public SqlParameter[][]? Parameters { set; get; }

        public int? Timeout { set; get; }
        public SqlTransaction? Transaction { set; get; }

        public bool? EnforceConstraints { set; get; }

        public static CommandInput NEWCommandInput(CommandInputs _ref)
        {
            return new CommandInput()
            {
                Execute = _ref.Execute,
                Query = _ref.Query,
                Parameters = _ref.Parameters?.FirstOrDefault(),
                Timeout = _ref.Timeout,
                Transaction = _ref.Transaction,
                EnforceConstraints = _ref.EnforceConstraints
            };
        }
    }
}
