using System.Data;

using imL.Contract.DB;

using Microsoft.Data.SqlClient;

namespace imL.Package.EFCSql
{
    public sealed class SqlParameterDefault : IParameter
    {
        public string Affect { get; }
        public string Expression { get; }
        public bool IsSearchCondition { get; }

        public SqlParameter Parameter { get; }

        public SqlParameterDefault(
            string _affect,
            object _value,
            SqlDbType _dbtype,
            bool _issearch = false
            )
        {
            Affect = _affect;
            IsSearchCondition = _issearch;

            Parameter = new SqlParameter
            {
                ParameterName = "@" + Affect,
                Value = _value ?? DBNull.Value,
                SqlDbType = _dbtype
            };

            Expression = Parameter.ParameterName;
        }
        public SqlParameterDefault(
            string _affect,
            object _value,
            SqlDbType _dbtype,
            int _size,
            bool _issearch = false
            ) : this(_affect, _value, _dbtype, _issearch)
        {
            Parameter.Size = _size;
        }
        public SqlParameterDefault(
            string _affect,
            object _value,
            SqlDbType _dbtype,
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
