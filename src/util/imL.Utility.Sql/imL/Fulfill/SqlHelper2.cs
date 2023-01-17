using imL.Contract.DB;
using imL.Enumeration.DB;

namespace imL.Utility.Sql
{
    public partial class SqlHelper
    {
        public Return Execute(string _query, EExecute _exe = EExecute.NonQuery)
        {
            IParameter[] _null = null;
            return Execute(_query, _exe, _null);
        }
        public Return[] Executions(string _query, EExecute _exe = EExecute.NonQuery)
        {
            IParameter[] _null = null;
            return Executions(_query, _exe, _null);
        }
    }
}