#if (NET35 || NET40) == false

#if (NETSTANDARD1_3) == false
using System.Data;
#endif

using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

using imL.Contract.DB;
using imL.Enumeration.DB;

using System.Collections.Generic;
using System.Linq;

namespace imL.Utility.Sql
{
    public class SqlHelperAsync : IHelperAsync
    {
        public IConnection Connection { get; }
        public bool Throw { get; }
        public IProgress<int> Progress { get; }

        public SqlHelperAsync(IConnection _conn, bool _throw = false, IProgress<int> _progress = null)
        {
            this.Connection = _conn;
            this.Throw = _throw;
            this.Progress = _progress;
        }

        public async Task<Return> ExecuteAsync(string _query, EExecute _exe = EExecute.NonQuery, params IParameter[] _pmts)
        {
            try
            {
                SqlConnectionDefault _conn_raw = (SqlConnectionDefault)this.Connection;
                IEnumerable<SqlParameter> _pmts_raw = _pmts.GetSqlParameters();

                using (SqlCommand _cmd = new SqlCommand(_query, _conn_raw.Connection))
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
                        case EExecute.XmlReader:
                            return new Return(true, await _cmd.ExecuteXmlReaderAsync(this.Connection.Token));
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
                SqlConnectionDefault _conn_raw = (SqlConnectionDefault)this.Connection;

                int _r = 0;
                Return[] _returns = new Return[_pmts.Length];
                IEnumerable<SqlParameter> _pmts_raw = _pmts[_r].GetSqlParameters();

                using (SqlCommand _cmd = new SqlCommand(_query, _conn_raw.Connection))
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
                                case EExecute.XmlReader:
                                    _returns[_r] = new Return(true, await _cmd.ExecuteXmlReaderAsync(this.Connection.Token));
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

#if (NET45_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)

        public async Task<DataTable> LoadDataTableAsync(string _query, params IParameter[] _pmts)
        {
            try
            {
                Return _exe = await ExecuteAsync(_query, EExecute.Reader, _pmts);
                _exe.TriggerErrorException();

                DataTable _return = new DataTable("DataTable_0");

                using (SqlDataReader _read = (SqlDataReader)_exe.Result)
                    _return.Load(_read, LoadOption.OverwriteChanges);

                return _return;
            }
            catch (Exception)
            {
                if (this.Throw)
                    throw;
            }

            return null;
        }
        public async Task<DataSet> LoadDataSetAsync(string _query, params IParameter[] _pmts)
        {
            try
            {
                Return _exe = await ExecuteAsync(_query, EExecute.Reader, _pmts);
                _exe.TriggerErrorException();

                DataSet _return = new DataSet("DataSet_0") { EnforceConstraints = this.Connection.Constraints };
                byte _n = 0;

                using (SqlDataReader _read = (SqlDataReader)_exe.Result)
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

                return _return;
            }
            catch (Exception)
            {
                if (this.Throw)
                    throw;
            }

            return null;
        }
        public async Task<G[]> LoadDataAsync<G>(string _query, params IParameter[] _pmts)
        {
            try
            {
                using (DataTable _dt = await LoadDataTableAsync(_query, _pmts))
                {
                    List<G> _return = new List<G>();
                    Settler<G> _set = new Settler<G>();

                    foreach (DataRow _item in _dt.Rows)
                        _return.Add(_set.Instance(_item));

                    return _return.ToArray();
                }
            }
            catch (Exception)
            {
                if (this.Throw)
                    throw;
            }

            return null;
        }

#endif

    }
}

#endif