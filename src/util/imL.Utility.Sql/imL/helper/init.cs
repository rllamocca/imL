#if (NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6) == false
using System.Data;
#endif

using System;

using imL.DB;

namespace imL.Utility.Sql
{
    public partial class SqlHelper : IHelper
    {
        public IConnection Connection { get; }
        public bool? Throw { get; }
        public IProgress<int> Progress { get; }

        public SqlHelper(IConnection _conn, bool _throw = false, IProgress<int> _progress = null)
        {
            Connection = _conn;
            Throw = _throw;
            Progress = _progress;
        }
    }
}