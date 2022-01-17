#if (NET35 || NET40) == false
using System.Threading;
using System.Threading.Tasks;
#endif

using imL.Contract.DB;

using MySql.Data.MySqlClient;

using System;
using System.Data;
using System.Data.Common;

namespace imL.Package.MySql
{
    public class MySqlConnectionDefault : IConnection
    {
        private bool _DISPOSED = false;
        //private bool _STATISTICS = false;

        private MySqlTransaction _TS;
        private MySqlConnection _CN;

        public MySqlTransaction Transaction
        {
            set { this._TS = value; }
            get { return this._TS; }
        }
        public MySqlConnection Connection
        {
            get { return this._CN; }
        }

        public MySqlConnectionDefault(DbConnection _conn)
        {
            this._CN = (MySqlConnection)_conn;
        }
        public MySqlConnectionDefault(MySqlConnection _conn)
        {
            this._CN = _conn;
        }
        public MySqlConnectionDefault(string _conn)
        {
            this._CN = new MySqlConnection(_conn);
        }

        //####
        public int TimeOut { set; get; } = 100;
        public bool Constraints { set; get; } = false;

#if (NET35 || NET40) == false
        public CancellationToken Token { set; get; } = default;
#endif

        public void Open()
        {
            switch (this._CN.State)
            {
                case ConnectionState.Closed:
                case ConnectionState.Broken:
                    this._CN.Open();

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

#if (NET35 || NET40) == false
        public async Task OpenAsync()
        {
            switch (this._CN.State)
            {
                case ConnectionState.Closed:
                case ConnectionState.Broken:
                    await this._CN.OpenAsync();

                    break;
                default:
                    break;
            }
        }
#endif
#if (NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER)
        public async Task CloseAsync()
        {
            if (this._CN.State != ConnectionState.Closed)
                await this._CN.CloseAsync();
        }
#endif
#if (NET35 || NET40) == false
        public async Task RefreshAsync()
        {
            await this.OpenAsync();
#if (NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER)
            await this.CloseAsync();
#else
            this.Close();
#endif
        }
#endif


        //################################################################################
        ~MySqlConnectionDefault()
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

                this.TimeOut = 0;
                this.Constraints = false;
            }

            this._DISPOSED = true;
        }
    }
}
