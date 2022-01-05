﻿#if (NET35 || NET40) == false
using System.Threading;
using System.Threading.Tasks;
#endif

using System;
using System.Data;

using imL.Contract.DB;

using MySql.Data.MySqlClient;

namespace imL.Package.MySql.Fulfill
{
    public class ConnectionDefault : IConnection
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

        public ConnectionDefault(MySqlConnection _conn)
        {
            this._CN = _conn;
        }
        public ConnectionDefault(string _conn)
        {
            this._CN = new MySqlConnection(_conn);
        }

        //####
        public int TimeOut { set; get; } = 100;
        public bool Constraints { set; get; } = false;
#if (NET35 || NET40) == false
        public CancellationToken Token { set; get; } = default;
#endif

        public void Close()
        {
            if (this._CN.State != ConnectionState.Closed)
                this._CN.Close();
        }

        //################################################################################
        ~ConnectionDefault()
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
    }
}