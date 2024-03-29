﻿using System.Collections.Generic;

namespace imL.JavaScript.ChartJS
{
    public class SerieFormat
    {
        public IEnumerable<decimal?> Values { set; get; }
        public string Name { set; get; }
        public string Stack { set; get; }

        public SerieFormat(IEnumerable<decimal?> _values, string _name = null, string _stack = null)
        {
            Values = _values;
            Name = _name;
            Stack = _stack;
        }
    }
}