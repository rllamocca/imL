using System;

using imL.DB;

using MySql.Data.MySqlClient;

namespace imL.Package.MySql
{
    public sealed class MySqlParameterDefault : IParameter
    {
        public string Affect { get; }
        public string Expression { get; }
        public bool? IsSearchCondition { get; }

        public MySqlParameter Parameter { get; }

        public MySqlParameterDefault(
            string _affect,
            object _value,
            MySqlDbType _dbtype,
            bool _issearch = false
            )
        {
            Affect = _affect;
            IsSearchCondition = _issearch;

            Parameter = new MySqlParameter
            {
                ParameterName = "@" + Affect,
                Value = _value ?? DBNull.Value,
                MySqlDbType = _dbtype
            };

            Expression = Parameter.ParameterName;
        }
        public MySqlParameterDefault(
            string _affect,
            object _value,
            MySqlDbType _dbtype,
            int _size,
            bool _issearch = false
            ) : this(_affect, _value, _dbtype, _issearch)
        {
            Parameter.Size = _size;
        }
        public MySqlParameterDefault(
            string _affect,
            object _value,
            MySqlDbType _dbtype,
            byte _precision,
            byte _scale,
            bool _issearch = false
            ) : this(_affect, _value, _dbtype, _issearch)
        {
            Parameter.Precision = _precision;
            Parameter.Scale = _scale;
        }

        public object GetValue()
        {
            return Parameter.Value;
        }
    }
}
