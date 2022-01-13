﻿#if (NET35 || NET40) == false

#if (NETSTANDARD1_3) == false
using System.Data;
#endif

using System;
using System.Threading.Tasks;

using imL.Contract.DB;
using imL.Enumeration.DB;

using MySql.Data.MySqlClient;

using System.Collections.Generic;
using System.Linq;

using imL.Utility;

namespace imL.Package.MySql
{
    public class MySqlHelperAsync : IHelperAsync
    {
        public IConnection Connection { get; }
        public bool Throw { get; }
        public IProgress<int> Progress { get; }

        public MySqlHelperAsync(IConnection _conn, bool _throw = false, IProgress<int> _progress = null)
        {
            this.Connection = _conn;
            this.Throw = _throw;
            this.Progress = _progress;
        }

        public async Task<Return> ExecuteAsync(string _query, EExecute _exe = EExecute.NonQuery, params IParameter[] _pmts)
        {
            try
            {
                MySqlConnectionDefault _conn_raw = (MySqlConnectionDefault)this.Connection;
                IEnumerable<MySqlParameter> _pmts_raw = _pmts.GetMySqlParameters();

                using (MySqlCommand _cmd = new MySqlCommand(_query, _conn_raw.Connection))
                {
                    _cmd.Transaction = _conn_raw.Transaction;
                    _cmd.CommandTimeout = this.Connection.TimeOut;

                    if (_pmts_raw.HasValue())
                        _cmd.Parameters.AddRange(_pmts_raw.ToArray());

                    switch (_exe)
                    {
                        case EExecute.NonQuery:
                            return new Return(true, await _cmd.ExecuteNonQueryAsync(this.Connection.Token));
                        case EExecute.Scalar:
                            return new Return(true, await _cmd.ExecuteScalarAsync(this.Connection.Token));
                        case EExecute.Reader:
                            return new Return(true, await _cmd.ExecuteReaderAsync(this.Connection.Token));
                        default:
                            return new Return(false);
                    }
                }
            }
            catch (Exception _ex)
            {
                if (this.Throw)
                    throw;

                return new Return(false, _ex);
            }
        }

        public async Task<Return[]> ExecuteAsync(string _query, EExecute _exe = EExecute.NonQuery, params IParameter[][] _pmts)
        {
            try
            {
                MySqlConnectionDefault _conn_raw = (MySqlConnectionDefault)this.Connection;

                int _r = 0;
                Return[] _returns = new Return[_pmts.Length];
                IEnumerable<MySqlParameter> _pmts_raw = _pmts[_r].GetMySqlParameters();

                using (MySqlCommand _cmd = new MySqlCommand(_query, _conn_raw.Connection))
                {
                    _cmd.Transaction = _conn_raw.Transaction;
                    _cmd.CommandTimeout = this.Connection.TimeOut;
                    _cmd.Parameters.AddRange(_pmts_raw.ToArray());

                    int _c_p = _cmd.Parameters.Count;
                    int _c_r = _returns.Length;

                    _cmd.Prepare();
                    do
                    {
                        try
                        {
                            if (_r > 0)
                                for (int _i = 0; _i < _c_p; _i++)
                                    _cmd.Parameters[_i].Value = _pmts[_r][_i].GetValue();

                            switch (_exe)
                            {
                                case EExecute.NonQuery:
                                    _returns[_r] = new Return(true, await _cmd.ExecuteNonQueryAsync(this.Connection.Token));
                                    break;
                                case EExecute.Scalar:
                                    _returns[_r] = new Return(true, await _cmd.ExecuteScalarAsync(this.Connection.Token));
                                    break;
                                case EExecute.Reader:
                                    _returns[_r] = new Return(true, await _cmd.ExecuteReaderAsync(this.Connection.Token));
                                    break;
                                default:
                                    _returns[_r] = new Return(false);
                                    break;
                            }
                        }
                        catch (Exception _ex)
                        {
                            if (this.Throw)
                                throw;

                            _returns[_r] = new Return(false, _ex);
                        }

                        _r++;
                        this.Progress?.Report(_r);

                    } while (_r < _c_r);
                }
                return _returns;
            }
            catch (Exception _ex)
            {
                if (this.Throw)
                    throw;

                return new Return[] { new Return(false, _ex) };
            }
        }

#if (NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6) == false

        public async Task<Return> LoadDataTableAsync(string _query, params IParameter[] _pmts)
        {
            try
            {
                Return _exe = await ExecuteAsync(_query, EExecute.Reader, _pmts);
                _exe.TriggerErrorException();

                DataTable _return = new DataTable("DataTable_0");

                using (MySqlDataReader _read = (MySqlDataReader)_exe.Result)
                    _return.Load(_read, LoadOption.OverwriteChanges);

                return new Return(true, _return);
            }
            catch (Exception _ex)
            {
                if (this.Throw)
                    throw;

                return new Return(false, _ex);
            }
        }
        public async Task<Return> LoadDataSetAsync(string _query, params IParameter[] _pmts)
        {
            try
            {
                Return _exe = await ExecuteAsync(_query, EExecute.Reader, _pmts);
                _exe.TriggerErrorException();

                DataSet _return = new DataSet("DataSet_0") { EnforceConstraints = this.Connection.Constraints };
                byte _n = 0;

                using (MySqlDataReader _read = (MySqlDataReader)_exe.Result)
                {
                    while (_read.IsClosed == false)
                    {
                        DataTable _dt = new DataTable("DataTable_" + Convert.ToString(_n));
                        _dt.Load(_read, LoadOption.OverwriteChanges);
                        _return.Tables.Add(_dt);
                        _n++;

                        this.Progress?.Report(_n);
                    }
                }

                return new Return(true, _return);
            }
            catch (Exception _ex)
            {
                if (this.Throw)
                    throw;

                return new Return(false, _ex);
            }
        }

#endif

    }
}

#endif