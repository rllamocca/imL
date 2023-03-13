#if (NET35 || NET40) == false

#if (NETSTANDARD1_3) == false
using System.Data;
#endif

using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

using imL.DB;

using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace imL.Utility.Sql
{
    public partial class SqlHelper
    {
        public async Task<Return> ExecuteAsync(string _query, EExecute _exe = EExecute.NonQuery, CancellationToken _ct = default, params IParameter[] _pmts)
        {
            try
            {
                SqlConnectionDefault _conn_raw = (SqlConnectionDefault)Connection;
                IEnumerable<SqlParameter> _pmts_raw = _pmts.GetSqlParameters();

                using (SqlCommand _cmd = new SqlCommand(_query, _conn_raw.Connection))
                {
                    _cmd.Transaction = _conn_raw.Transaction;
                    _cmd.CommandTimeout = Connection.TimeOut ?? _cmd.CommandTimeout;

                    if (_pmts_raw.HasValue())
                        _cmd.Parameters.AddRange(_pmts_raw.ToArray());

                    switch (_exe)
                    {
                        case EExecute.NonQuery:
                            return new Return(true, await _cmd.ExecuteNonQueryAsync(_ct));
                        case EExecute.Scalar:
                            return new Return(true, await _cmd.ExecuteScalarAsync(_ct));
                        case EExecute.Reader:
                            return new Return(true, await _cmd.ExecuteReaderAsync(_ct));
                        case EExecute.XmlReader:
                            return new Return(true, await _cmd.ExecuteXmlReaderAsync(_ct));
                        default:
                            return new Return(false);
                    }
                }
            }
            catch (Exception _ex)
            {
                if (Throw == true)
                    throw;

                return new Return(false, _ex);
            }
        }
        public async Task<Return> ExecuteAsync(string _query, EExecute _exe = EExecute.NonQuery, CancellationToken _ct = default)
        {
            return await ExecuteAsync(_query, _exe, _ct, null);
        }

        public async Task<IEnumerable<Return>> ExecutionsAsync(string _query, EExecute _exe = EExecute.NonQuery, CancellationToken _ct = default, params IParameter[][] _pmts)
        {
            try
            {
                SqlConnectionDefault _conn_raw = (SqlConnectionDefault)Connection;

                int _r = 0;
                Return[] _returns = new Return[_pmts.Length];
                IEnumerable<SqlParameter> _pmts_raw = _pmts[_r].GetSqlParameters();

                using (SqlCommand _cmd = new SqlCommand(_query, _conn_raw.Connection))
                {
                    _cmd.Transaction = _conn_raw.Transaction;
                    _cmd.CommandTimeout = Connection.TimeOut ?? _cmd.CommandTimeout;
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
                                    _returns[_r] = new Return(true, await _cmd.ExecuteNonQueryAsync(_ct));
                                    break;
                                case EExecute.Scalar:
                                    _returns[_r] = new Return(true, await _cmd.ExecuteScalarAsync(_ct));
                                    break;
                                case EExecute.Reader:
                                    _returns[_r] = new Return(true, await _cmd.ExecuteReaderAsync(_ct));
                                    break;
                                case EExecute.XmlReader:
                                    _returns[_r] = new Return(true, await _cmd.ExecuteXmlReaderAsync(_ct));
                                    break;
                                default:
                                    _returns[_r] = new Return(false);
                                    break;
                            }
                        }
                        catch (Exception _ex)
                        {
                            if (Throw == true)
                                throw;

                            _returns[_r] = new Return(false, _ex);
                        }

                        _r++;
                        Progress?.Report(_r);

                    } while (_r < _c_r);
                }
                return _returns;
            }
            catch (Exception _ex)
            {
                if (Throw == true)
                    throw;

                return new Return[] { new Return(false, _ex) };
            }
        }
        public async Task<IEnumerable<Return>> ExecutionsAsync(string _query, EExecute _exe = EExecute.NonQuery, CancellationToken _ct = default)
        {
            return await ExecutionsAsync(_query, _exe, _ct, null);
        }

#if (NET45_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)

        public async Task<DataTable> LoadDataTableAsync(string _query, CancellationToken _ct = default, params IParameter[] _pmts)
        {
            try
            {
                Return _exe = await ExecuteAsync(_query, EExecute.Reader, _ct, _pmts);
                _exe.TriggerErrorException();

                DataTable _return = new DataTable("DataTable_0");

                using (SqlDataReader _read = (SqlDataReader)_exe.Result)
                    _return.Load(_read, LoadOption.OverwriteChanges);

                return _return;
            }
            catch (Exception)
            {
                if (Throw == true)
                    throw;
            }

            return null;
        }
        public async Task<DataTable> LoadDataTableAsync(string _query, CancellationToken _ct = default)
        {
            return await LoadDataTableAsync(_query, _ct, null);
        }
        public async Task<DataSet> LoadDataSetAsync(string _query, CancellationToken _ct = default, params IParameter[] _pmts)
        {
            try
            {
                Return _exe = await ExecuteAsync(_query, EExecute.Reader, _ct, _pmts);
                _exe.TriggerErrorException();

                DataSet _return = new DataSet("DataSet_0") { EnforceConstraints = Connection.Constraints.GetValueOrDefault() };
                byte _n = 0;

                using (SqlDataReader _read = (SqlDataReader)_exe.Result)
                {
                    while (_read.IsClosed == false)
                    {
                        DataTable _dt = new DataTable("DataTable_" + Convert.ToString(_n));
                        _dt.Load(_read, LoadOption.OverwriteChanges);
                        _return.Tables.Add(_dt);
                        _n++;

                        Progress?.Report(_n);
                    }
                }

                return _return;
            }
            catch (Exception)
            {
                if (Throw == true)
                    throw;
            }

            return null;
        }
        public async Task<DataSet> LoadDataSetAsync(string _query, CancellationToken _ct = default)
        {
            return await LoadDataSetAsync(_query, _ct, null);
        }
        public async Task<IEnumerable<G>> LoadDataAsync<G>(string _query, CancellationToken _ct = default, params IParameter[] _pmts)
        {
            try
            {
                using (DataTable _dt = await LoadDataTableAsync(_query, _ct, _pmts))
                {
                    IList<G> _return = new List<G>();
                    Setter<G> _set = new Setter<G>();

                    foreach (DataRow _item in _dt.Rows)
                        _return.Add(_set.Instance(_item));

                    return _return;
                }
            }
            catch (Exception)
            {
                if (Throw == true)
                    throw;
            }

            return null;
        }
        public async Task<IEnumerable<G>> LoadDataAsync<G>(string _query, CancellationToken _ct = default)
        {
            return await LoadDataAsync<G>(_query, _ct, null);
        }

#endif

    }
}

#endif