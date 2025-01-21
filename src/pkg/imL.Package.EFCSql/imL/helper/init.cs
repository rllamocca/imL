using imL.DB;

namespace imL.Package.EFCSql
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