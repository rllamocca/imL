using System;

namespace imL.Utility
{
    public static class RandomHelper
    {
        public static int?[] Int32s(int _length, int _min = -100, int _max = 100)
        {
            int?[] _return = new int?[_length];
            _max++;
            Random _random = new Random();

            for (int _i = 0; _i < _length; _i++)
                _return[_i] = _random.Next(_min, _max);

            return _return;
        }
        public static decimal?[] Decimals(int _length, int _min = -100, int _max = 100)
        {
            decimal?[] _return = new decimal?[_length];
            int _decimal = 100;
            _min *= _decimal;
            _max *= _decimal;
            _max++;
            Random _random = new Random();

            for (int _i = 0; _i < _length; _i++)
            {
                int _n = _random.Next(_min, _max);
                _return[_i] = Convert.ToDecimal(_n * 1.0 / _decimal);
            }

            return _return;
        }
    }
}
