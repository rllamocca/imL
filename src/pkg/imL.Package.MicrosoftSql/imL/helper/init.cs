using System;

using imL.DB;

namespace imL.Package.MicrosoftSql
{
    public partial class MicrosoftSqlHelper : IHelper
    {
        public IConnection Connection { get; }
        public bool? Throw { get; }
        public IProgress<int> Progress { get; }

        public MicrosoftSqlHelper(IConnection _conn, bool _throw = false, IProgress<int> _progress = null)
        {
            Connection = _conn;
            Throw = _throw;
            Progress = _progress;
        }
    }
}