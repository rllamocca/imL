#if (NET45 || NETSTANDARD1_3 || NETSTANDARD2_0)

#if NETSTANDARD1_3 == false
using System.Data;
#endif

using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

using imL.Contract;
using imL.Contract.DB;
using imL.Enumeration.DB;

namespace imL.Utility.Sql.UserModel
{
    public class SqlHelperAsync : IHelperAsync
    {
        public IConnection Connection { get; }
        public bool EThrow { get; }
        public IProgress<int> Progress { get; }

        public SqlHelperAsync(IConnection _conn, bool _throw = false, IProgress<int> _progress = null)
        {
            this.Connection = _conn;
            this.EThrow = _throw;
            this.Progress = _progress;
        }

        public async Task<Return> Execute(string _query, EExecute _exe = EExecute.NonQuery, params IParameter[] _pmts)
        {
            try
            {
                ISqlConnection _conn_raw = (ISqlConnection)Connection;
                SqlParameter[] _pmts_raw = _pmts.GetParameters().GetSqlParameters();

                using (SqlCommand _cmd = new SqlCommand(_query, _conn_raw.Connection))
                {
                    _cmd.Transaction = _conn_raw.Transaction;
                    _cmd.CommandTimeout = Connection.TimeOut;

                    if (_pmts_raw != null)
                        _cmd.Parameters.AddRange(_pmts_raw);

                    switch (_exe)
                    {
                        case EExecute.NonQuery:
                            return new Return(true, await _cmd.ExecuteNonQueryAsync(Connection.Token));
                        case EExecute.Scalar:
                            return new Return(true, await _cmd.ExecuteScalarAsync(Connection.Token));
                        case EExecute.Reader:
                            return new Return(true, await _cmd.ExecuteReaderAsync(Connection.Token));
                        case EExecute.XmlReader:
                            return new Return(true, await _cmd.ExecuteXmlReaderAsync(Connection.Token));
                        default:
                            return new Return(false);
                    }
                }
            }
            catch (Exception _ex)
            {
                if (EThrow)
                    throw _ex;

                return new Return(false, _ex);
            }
        }

        public async Task<Return[]> Execute(string _query, EExecute _exe = EExecute.NonQuery, params IParameter[][] _pmts)
        {
            try
            {
                ISqlConnection _conn_raw = (ISqlConnection)Connection;

                int _r = 0;
                Return[] _returns = new Return[_pmts.Length];
                SqlParameter[] _pmts_raw = _pmts[_r].GetParameters().GetSqlParameters();

                using (SqlCommand _cmd = new SqlCommand(_query, _conn_raw.Connection))
                {
                    _cmd.Transaction = _conn_raw.Transaction;
                    _cmd.CommandTimeout = Connection.TimeOut;
                    _cmd.Parameters.AddRange(_pmts_raw);

                    int _c_p = _cmd.Parameters.Count;
                    int _c_r = _returns.Length;

                    _cmd.Prepare();
                    do
                    {
                        try
                        {
                            if (_r > 0)
                                for (int _i = 0; _i < _c_p; _i++)
                                    _cmd.Parameters[_i].Value = _pmts[_r][_i].Value;

                            switch (_exe)
                            {
                                case EExecute.NonQuery:
                                    _returns[_r] = new Return(true, await _cmd.ExecuteNonQueryAsync(Connection.Token));
                                    break;
                                case EExecute.Scalar:
                                    _returns[_r] = new Return(true, await _cmd.ExecuteScalarAsync(Connection.Token));
                                    break;
                                case EExecute.Reader:
                                    _returns[_r] = new Return(true, await _cmd.ExecuteReaderAsync(Connection.Token));
                                    break;
                                case EExecute.XmlReader:
                                    _returns[_r] = new Return(true, await _cmd.ExecuteXmlReaderAsync(Connection.Token));
                                    break;
                                default:
                                    _returns[_r] = new Return(false);
                                    break;
                            }
                        }
                        catch (Exception _ex)
                        {
                            if (EThrow)
                                throw _ex;

                            _returns[_r] = new Return(false, _ex);
                        }
                        _r++;
                        Progress?.Report(0);
                    } while (_r < _c_r);
                }
                return _returns;
            }
            catch (Exception _ex)
            {
                if (EThrow)
                    throw _ex;

                return new Return[] { new Return(false, _ex) };
            }
        }

#if NETSTANDARD1_3 == false

        public async Task<Return> LoadData(string _query, bool _dataset = true, params IParameter[] _pmts)
        {
            try
            {
                Return _exe = await Execute(_query, EExecute.Reader, _pmts);
                _exe.TriggerErrorException();

                if (_dataset)
                {
                    DataSet _return = new DataSet("DataSet_0") { EnforceConstraints = Connection.Constraints };
                    byte _n = 0;

                    using (SqlDataReader _read = (SqlDataReader)_exe.Result)
                    {
                        while (_read.IsClosed == false)
                        {
                            DataTable _dt = new DataTable("DataTable_" + Convert.ToString(_n));
                            _dt.Load(_read, LoadOption.OverwriteChanges);
                            _return.Tables.Add(_dt);
                            _n++;

                            Progress?.Report(0);
                        }
                    }

                    return new Return(true, _return);
                }
                else
                {
                    DataTable _return = new DataTable("DataTable_0");

                    using (SqlDataReader _read = (SqlDataReader)_exe.Result)
                        _return.Load(_read, LoadOption.OverwriteChanges);

                    return new Return(true, _return);
                }
            }
            catch (Exception _ex)
            {
                if (EThrow)
                    throw _ex;

                return new Return(false, _ex);
            }
        }

#endif

    }
}

#endif