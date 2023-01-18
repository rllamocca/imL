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
        bool _DISPOSED = false;
        //bool _STATISTICS = false;

        MySqlTransaction _TS;
        MySqlConnection _CN;

        public MySqlTransaction Transaction
        {
            set { _TS = value; }
            get { return _TS; }
        }
        public MySqlConnection Connection
        {
            get { return _CN; }
        }

        public MySqlConnectionDefault(DbConnection _conn)
        {
            _CN = (MySqlConnection)_conn;
        }
        public MySqlConnectionDefault(MySqlConnection _conn)
        {
            _CN = _conn;
        }
        public MySqlConnectionDefault(string _conn)
        {
            _CN = new MySqlConnection(_conn);
        }

        //####
        public int TimeOut { set; get; } = 100;
        public bool Constraints { set; get; } = false;

#if (NET35 || NET40) == false
        public CancellationToken Token { set; get; } = default;
#endif

        public void Open()
        {
            switch (_CN.State)
            {
                case ConnectionState.Closed:
                case ConnectionState.Broken:
                    _CN.Open();

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

#if (NET35 || NET40) == false
        public async Task OpenAsync()
        {
            switch (_CN.State)
            {
                case ConnectionState.Closed:
                case ConnectionState.Broken:
                    await _CN.OpenAsync();

                    break;
                default:
                    break;
            }
        }
#endif
#if (NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER)
        public async Task CloseAsync()
        {
            if (_CN.State != ConnectionState.Closed)
                await _CN.CloseAsync();
        }
#endif
#if (NET35 || NET40) == false
        public async Task RefreshAsync()
        {
            await OpenAsync();
#if (NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER)
            await CloseAsync();
#else
            Close();
#endif
        }
#endif


        //################################################################################
        ~MySqlConnectionDefault()
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

                TimeOut = 0;
                Constraints = false;
            }

            _DISPOSED = true;
        }
    }
}
