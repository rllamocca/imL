using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

using imL.DB;

using Microsoft.Data.SqlClient;

namespace imL.Package.MicrosoftSql
{
    public class MicrosoftSqlConnectionDefault : IConnection
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

        public MicrosoftSqlConnectionDefault(DbConnection _conn)
        {
            _CN = (SqlConnection)_conn;
        }
        public MicrosoftSqlConnectionDefault(SqlConnection _conn)
        {
            _STATISTICS = _CN.StatisticsEnabled;
            _CN = _conn;
        }
        public MicrosoftSqlConnectionDefault(string _conn, bool _stat = false)
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
        public int? TimeOut { set; get; }
        public bool? Constraints { set; get; }

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

#if NETCOREAPP || NETSTANDARD2_1_OR_GREATER
        public async Task CloseAsync()
        {
            if (_CN.State != ConnectionState.Closed)
                await _CN.CloseAsync();


        }
#endif

        public async Task RefreshAsync()
        {
            await OpenAsync();

#if NETCOREAPP || NETSTANDARD2_1_OR_GREATER
            await CloseAsync();
#else
            Close();
#endif

        }

        //################################################################################
        ~MicrosoftSqlConnectionDefault()
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
