using System;
using System.Collections.Generic;
using System.Linq;

using System.Data;

using imL.DB;

using MySql.Data.MySqlClient;

namespace imL.Package.MySql
{
    public partial class MySqlHelper : IHelper
    {
        public Return Execute(string _query, EExecute _exe = EExecute.NonQuery, params IParameter[] _pmts)
        {
            try
            {
                MySqlConnectionDefault _conn_raw = (MySqlConnectionDefault)Connection;
                IEnumerable<MySqlParameter> _pmts_raw = _pmts.GetMySqlParameters();

                using (MySqlCommand _cmd = new MySqlCommand(_query, _conn_raw.Connection))
                {
                    _cmd.Transaction = _conn_raw.Transaction;
                    _cmd.CommandTimeout = Connection.TimeOut ?? _cmd.CommandTimeout;

                    if (_pmts_raw.HasValue())
                        _cmd.Parameters.AddRange(_pmts_raw.ToArray());

                    switch (_exe)
                    {
                        case EExecute.NonQuery:
                            return new Return(true, _cmd.ExecuteNonQuery());
                        case EExecute.Scalar:
                            return new Return(true, _cmd.ExecuteScalar());
                        case EExecute.Reader:
                            return new Return(true, _cmd.ExecuteReader());
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
        public Return Execute(string _query, EExecute _exe = EExecute.NonQuery)
        {
            return Execute(_query, _exe, null);
        }

        public IEnumerable<Return> Executions(string _query, EExecute _exe = EExecute.NonQuery, params IParameter[][] _pmts)
        {
            try
            {
                MySqlConnectionDefault _conn_raw = (MySqlConnectionDefault)Connection;

                int _r = 0;
                Return[] _returns = new Return[_pmts.Length];
                IEnumerable<MySqlParameter> _pmts_raw = _pmts[_r].GetMySqlParameters();

                using (MySqlCommand _cmd = new MySqlCommand(_query, _conn_raw.Connection))
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
                                    _returns[_r] = new Return(true, _cmd.ExecuteNonQuery());
                                    break;
                                case EExecute.Scalar:
                                    _returns[_r] = new Return(true, _cmd.ExecuteScalar());
                                    break;
                                case EExecute.Reader:
                                    _returns[_r] = new Return(true, _cmd.ExecuteReader());
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
        public IEnumerable<Return> Executions(string _query, EExecute _exe = EExecute.NonQuery)
        {
            return Executions(_query, _exe, null);
        }

#if (NET35_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)

        public DataTable LoadDataTable(string _query, params IParameter[] _pmts)
        {
            try
            {
                Return _exe = Execute(_query, EExecute.Reader, _pmts);
                _exe.TriggerErrorException();

                DataTable _return = new DataTable("DataTable_0");

                using (MySqlDataReader _read = (MySqlDataReader)_exe.Result)
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
        public DataTable LoadDataTable(string _query)
        {
            return LoadDataTable(_query, null);
        }
        public DataSet LoadDataSet(string _query, params IParameter[] _pmts)
        {
            try
            {
                Return _exe = Execute(_query, EExecute.Reader, _pmts);
                _exe.TriggerErrorException();

                DataSet _return = new DataSet("DataSet_0") { EnforceConstraints = Connection.Constraints.GetValueOrDefault() };
                byte _n = 0;

                using (MySqlDataReader _read = (MySqlDataReader)_exe.Result)
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
        public DataSet LoadDataSet(string _query)
        {
            return LoadDataSet(_query, null);
        }
        public IEnumerable<G> LoadData<G>(string _query, params IParameter[] _pmts)
        {
            try
            {
                using (DataTable _dt = LoadDataTable(_query, _pmts))
                {
                    List<G> _return = new List<G>();
                    Setter<G> _set = new Setter<G>();

                    foreach (DataRow _item in _dt.Rows)
                        _return.Add(_set.Instance(_item));

                    return _return.ToArray();
                }
            }
            catch (Exception)
            {
                if (Throw == true)
                    throw;
            }

            return null;
        }
        public IEnumerable<G> LoadData<G>(string _query)
        {
            return LoadData<G>(_query, null);
        }

#endif

    }
}