using System;

using imL.Contract.DB;

using MySql.Data.MySqlClient;

namespace imL.Package.MySql
{
    public sealed class MySqlParameterDefault : IParameter
    {
        public string Affect { get; }
        public string Expression { get; }
        public bool SkipEffect { get; }

        public MySqlParameter Parameter { get; }

        public MySqlParameterDefault(
            string _affect,
            object _value,
            MySqlDbType _dbtype,
            bool _skipeffect = false
            )
        {
            this.Affect = _affect;
            this.SkipEffect = _skipeffect;

            this.Parameter = new MySqlParameter
            {
                ParameterName = "@" + this.Affect,
                Value = _value ?? DBNull.Value,
                MySqlDbType = _dbtype
            };

            if (this.SkipEffect == false)
                this.Expression = this.Parameter.ParameterName;
        }
        public MySqlParameterDefault(
            string _affect,
            object _value,
            MySqlDbType _dbtype,
            int _size,
            bool _skipeffect = false
            ) : this(_affect, _value, _dbtype, _skipeffect)
        {
            this.Parameter.Size = _size;
        }
        public MySqlParameterDefault(
            string _affect,
            object _value,
            MySqlDbType _dbtype,
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
