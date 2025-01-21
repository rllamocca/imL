using System;
using System.Collections.Generic;
using System.Data;

using Microsoft.Data.SqlClient;

namespace imL.NC.pkg.MicrosoftEntityFrameworkCoreSqlServer
{
    public static partial class SqlConnectionExtension
    {
        static Return Execute(SqlCommand _c, EExecute? _e)
        {
            switch (_e)
            {
                case EExecute.NonQuery:
                    return new Return(true, _c.ExecuteNonQuery());
                case EExecute.Scalar:
                    return new Return(true, _c.ExecuteScalar());
                case EExecute.Reader:
                    return new Return(true, _c.ExecuteReader());
                case EExecute.XmlReader:
                    return new Return(true, _c.ExecuteXmlReader());
                default:
                    return new Return(false);
            }
        }
        public static Return TryExecute(this SqlConnection _this, CommandInput _input)
        {
            try
            {
                using SqlCommand _using = NEWSqlCommand(_this, _input);

                return Execute(_using, _input.Execute);
            }
            catch (Exception _ex)
            {
                return new Return(_ex);
            }
        }
        public static Return[] TryExecutes(this SqlConnection _this, CommandInputs _inputs)
        {
            try
            {
                using SqlCommand _using = NEWSqlCommand(_this, CommandInputs.NEWCommandInput(_inputs), true);

                if (_inputs.Parameters == null)
                    return new Return[] { Execute(_using, _inputs.Execute) };
                else
                    _using.Prepare(); 

                List<Return> _return = new List<Return>();

                foreach (SqlParameter[] _item in _inputs.Parameters)
                {
                    Return _add;

                    try
                    {
                        _using.OverwriteParameters(_item);
                        _add = Execute(_using, _inputs.Execute);
                    }
                    catch (Exception _ex)
                    {
                        _add = new Return(_ex);
                    }

                    _return.Add(_add);
                }

                return _return.ToArray();
            }
            catch (Exception _ex)
            {
                return new Return[] { new Return(_ex) };
            }
        }

        static Return? LoadData(SqlConnection _this, CommandInput _input)
        {
            _input.Execute = EExecute.Reader;
            Return _return = _this.TryExecute(_input);
            _return.TriggerErrorException();

            if (_return.Result == null)
                return null;

            return _return;
        }

        public static DataSet? LoadDataSet(this SqlConnection _this, CommandInput _input)
        {
            Return? _exe = LoadData(_this, _input);

            if (_exe == null || _exe.Result == null)
                return null;

            using SqlDataReader _using = (SqlDataReader)_exe.Result;
            return LOADDataSet(_using, _input);
        }
        public static DataTable? LoadDataTable(this SqlConnection _this, CommandInput _input)
        {
            Return? _exe = LoadData(_this, _input);

            if (_exe == null || _exe.Result == null)
                return null;

            using SqlDataReader _using = (SqlDataReader)_exe.Result;
            return LOADDataTable(_using);
        }
    }
}
