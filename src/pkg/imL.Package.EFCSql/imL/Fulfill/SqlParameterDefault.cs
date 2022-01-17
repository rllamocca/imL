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
            this.Affect = _affect;
            this.IsSearchCondition = _issearch;

            this.Parameter = new SqlParameter
            {
                ParameterName = "@" + this.Affect,
                Value = _value ?? DBNull.Value,
                SqlDbType = _dbtype
            };

            this.Expression = this.Parameter.ParameterName;
        }
        public SqlParameterDefault(
            string _affect,
            object _value,
            SqlDbType _dbtype,
            int _size,
            bool _issearch = false
            ) : this(_affect, _value, _dbtype, _issearch)
        {
            this.Parameter.Size = _size;
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
            this.Parameter.Precision = _precision;
            this.Parameter.Scale = _scale;
        }

        public object GetValue()
        {
            return this.Parameter.Value;
        }
    }
}
