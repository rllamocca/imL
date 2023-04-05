using System;
using System.Collections.Generic;
using System.Linq;

using System.Data;

using imL.DB;

using MySql.Data.MySqlClient;

namespace imL.Package.MySql
{
    public partial class MySqlHelper : IHelper
    {
        public IConnection Connection { get; }
        public bool? Throw { get; }
        public IProgress<int> Progress { get; }

        public MySqlHelper(IConnection _conn, bool _throw = false, IProgress<int> _progress = null)
        {
            Connection = _conn;
            Throw = _throw;
            Progress = _progress;
        }
    }
}