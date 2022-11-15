using System.Collections;
using System.Data;
using System.Data.Common;

using imL.Contract.DB;

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
            set { this._TS = value; }
            get { return this._TS; }
        }
        public SqlConnection Connection
        {
            get { return this._CN; }
        }

        public SqlConnectionDefault(DbConnection _conn)
        {
            this._CN = (SqlConnection)_conn;
        }
        public SqlConnectionDefault(SqlConnection _conn)
        {
            this._STATISTICS = this._CN.StatisticsEnabled;
            this._CN = _conn;
        }
        public SqlConnectionDefault(string _conn, bool _stat = false)
        {
            this._STATISTICS = _stat;
            this._CN = new SqlConnection(_conn)
            {
                StatisticsEnabled = this._STATISTICS
            };
        }
        public IDictionary RetrieveStatistics()
        {
            return _CN.RetrieveStatistics();
        }
        public void ResetStatistics()
        {
            this._CN.ResetStatistics();
        }

        //####
        public int TimeOut { set; get; } = 100;
        public bool Constraints { set; get; } = false;
        public CancellationToken Token { set; get; } = default;

        public void Open()
        {
            switch (this._CN.State)
            {
                case ConnectionState.Closed:
                case ConnectionState.Broken:
                    this._CN.Open();
                    this._CN.StatisticsEnabled = this._STATISTICS;

                    break;
                default:
                    break;
            }
        }
        public void Close()
        {
            if (this._CN.State != ConnectionState.Closed)
                this._CN.Close();
        }
        public void Refresh()
        {
            this.Open();
            this.Close();
        }

        public async Task OpenAsync()
        {
            switch (this._CN.State)
            {
                case ConnectionState.Closed:
                case ConnectionState.Broken:
                    await this._CN.OpenAsync();
                    this._CN.StatisticsEnabled = this._STATISTICS;

                    break;
                default:
                    break;
            }
        }
        public async Task CloseAsync()
        {
            if (this._CN.State != ConnectionState.Closed)
                await this._CN.CloseAsync();
        }
        public async Task RefreshAsync()
        {
            await this.OpenAsync();
            await this.CloseAsync();
        }

        //################################################################################
        ~SqlConnectionDefault()
        {
            this.Dispose(false);
        }
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool _managed)
        {
            if (this._DISPOSED)
                return;

            if (_managed)
            {
                if (this._TS != null)
                {
                    this._TS.Dispose();
                    this._TS = null;
                }
                if (this._CN != null)
                {
                    this._CN.Dispose();
                    this._CN = null;
                }

                this._STATISTICS = false;

                this.TimeOut = 0;
                this.Constraints = false;
            }

            this._DISPOSED = true;
        }
    }
}
