using System.Collections;
using System.Data;
using System.Data.Common;

using imL.DB;

using Microsoft.Data.SqlClient;

namespace imL.Package.EFCSql
{
    public class SqlConnectionDefault : IConnection
    {
        bool _DISPOSED = false;
        bool _STATISTICS = false;

        SqlTransaction _TS;
        SqlConnection _CN;

        public SqlTransaction Transaction
        {
            set { _TS = value; }
            get { return _TS; }
        }
        public SqlConnection Connection
        {
            get { return _CN; }
        }

        public SqlConnectionDefault(DbConnection _conn)
        {
            _CN = (SqlConnection)_conn;
        }
        public SqlConnectionDefault(SqlConnection _conn)
        {
            _STATISTICS = _CN.StatisticsEnabled;
            _CN = _conn;
        }
        public SqlConnectionDefault(string _conn, bool _stat = false)
        {
            _STATISTICS = _stat;
            _CN = new SqlConnection(_conn)
            {
                StatisticsEnabled = _STATISTICS
            };
        }
        public IDictionary RetrieveStatistics()
        {
            return _CN.RetrieveStatistics();
        }
        public void ResetStatistics()
        {
            _CN.ResetStatistics();
        }

        //####
        public int? TimeOut { set; get; } = 100;
        public bool? Constraints { set; get; } = false;
        public CancellationToken Token { set; get; } = default;

        public void Open()
        {
            switch (_CN.State)
            {
                case ConnectionState.Closed:
                case ConnectionState.Broken:
                    _CN.Open();
                    _CN.StatisticsEnabled = _STATISTICS;

                    break;
                default:
                    break;
            }
        }
        public void Close()
        {
            if (_CN.State != ConnectionState.Closed)
                _CN.Close();
        }
        public void Refresh()
        {
            Open();
            Close();
        }

        public async Task OpenAsync()
        {
            switch (_CN.State)
            {
                case ConnectionState.Closed:
                case ConnectionState.Broken:
                    await _CN.OpenAsync();
                    _CN.StatisticsEnabled = _STATISTICS;

                    break;
                default:
                    break;
            }
        }
        public async Task CloseAsync()
        {
            if (_CN.State != ConnectionState.Closed)
                await _CN.CloseAsync();
        }
        public async Task RefreshAsync()
        {
            await OpenAsync();
            await CloseAsync();
        }

        //################################################################################
        ~SqlConnectionDefault()
        {
            Dispose(false);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool _managed)
        {
            if (_DISPOSED)
                return;

            if (_managed)
            {
                if (_TS != null)
                {
                    _TS.Dispose();
                    _TS = null;
                }
                if (_CN != null)
                {
                    _CN.Dispose();
                    _CN = null;
                }

                _STATISTICS = false;

                TimeOut = 0;
                Constraints = false;
            }

            _DISPOSED = true;
        }
    }
}
