using System;
using System.Data;
using System.Data.SqlClient;

using imL.Contract.DB;

namespace imL.Utility.Sql
{
    public sealed class SqlParameterDefault : IParameter
    {
        public string Affect { get; }
        public string Expression { get; }
        public bool SkipEffect { get; }

        public SqlParameter Parameter { get; }

        public SqlParameterDefault(
            string _affect,
            object _value,
            SqlDbType _dbtype,
            bool _skipeffect = false
            )
        {
            this.Affect = _affect;
            //this.Value = _value ?? DBNull.Value;
            this.SkipEffect = _skipeffect;

            this.Parameter = new SqlParameter
            {
                ParameterName = "@" + this.Affect,
                Value = _value ?? DBNull.Value,
                SqlDbType = _dbtype
            };

            if (this.SkipEffect == false)
                this.Expression = this.Parameter.ParameterName;
        }
        public SqlParameterDefault(
            string _affect,
            object _value,
            SqlDbType _dbtype,
            int _size,
            bool _skipeffect = false
            ) : this(_affect, _value, _dbtype, _skipeffect)
        {
            this.Parameter.Size = _size;
        }
        public SqlParameterDefault(
            string _affect,
            object _value,
            SqlDbType _dbtype,
            byte _precision,
            byte _scale,
            bool _skipeffect = false
            ) : this(_affect, _value, _dbtype, _skipeffect)
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
