using imL.Contract.DB;

using MySql.Data.MySqlClient;

using System;
using System.Data;

namespace imL.Package.MySql
{
    public class ParameterDefault : IParameter
    {
        public string Source { set; get; }
        public object Value { set; get; }
        public string Affect { set; get; }

        public MySqlParameter Parameter { get; }

        public ParameterDefault(
            string _affect,
            string _function
            )
        {
            this.Source = _affect;
            this.Affect = _function;

            this.Value = DBNull.Value;
            this.Parameter = null;
        }
        public ParameterDefault(
            string _source,
            object _value,
            MySqlDbType _dbtype,
            ParameterDirection _direction = ParameterDirection.Input
            )
        {
            this.Source = _source;
            this.Value = _value ?? DBNull.Value;
            this.Affect = "@" + this.Source;

            this.Parameter = new MySqlParameter
            {
                ParameterName = this.Affect,
                Value = this.Value,
                MySqlDbType = _dbtype,
                Direction = _direction
            };
        }
        public ParameterDefault(
            string _source,
            object _value,
            MySqlDbType _dbtype,
            int _size,
            ParameterDirection _direction = ParameterDirection.Input
            ) : this(_source, _value, _dbtype, _direction)
        {
            this.Parameter.Size = _size;
        }
        public ParameterDefault(
            string _source,
            object _value,
            MySqlDbType _dbtype,
            byte _precision,
            byte _scale,
            ParameterDirection _direction = ParameterDirection.Input
            ) : this(_source, _value, _dbtype, _direction)
        {
            this.Parameter.Precision = _precision;
            this.Parameter.Scale = _scale;
        }
    }
}
