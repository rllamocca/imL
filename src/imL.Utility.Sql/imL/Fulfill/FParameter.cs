﻿using System;
using System.Data;
using System.Data.SqlClient;

using imL.Contract.DB;

namespace imL.Utility.Sql.Fulfill
{
    public class FParameter : IParameter
    {
        public string Source { set; get; }
        public object Value { set; get; }
        public string Affect { set; get; }

        public SqlParameter Parameter { get; }

        public FParameter(
            string _affect,
            string _function
            )
        {
            this.Source = _affect;
            this.Value = DBNull.Value;
            this.Affect = _function;

            this.Parameter = null;
        }
        public FParameter(
            string _source,
            object _value,
            SqlDbType _dbtype,
            ParameterDirection _direction = ParameterDirection.Input
            )
        {
            this.Source = _source;
            this.Value = _value ?? DBNull.Value;
            this.Affect = "@" + this.Source;

            this.Parameter = new SqlParameter
            {
                ParameterName = this.Affect,
                Value = this.Value,
                SqlDbType = _dbtype,
                Direction = _direction
            };
        }
        public FParameter(
            string _source,
            object _value,
            SqlDbType _dbtype,
            int _size,
            ParameterDirection _direction = ParameterDirection.Input
            ) : this(_source, _value, _dbtype, _direction)
        {
            this.Parameter.Size = _size;
        }
        public FParameter(
            string _source,
            object _value,
            SqlDbType _dbtype,
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