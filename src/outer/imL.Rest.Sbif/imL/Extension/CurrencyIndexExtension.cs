using System;
using System.Collections.Generic;
using System.Linq;

namespace imL.Rest.SBIF
{
    public static class CurrencyIndexExtension
    {
        public static CurrencyIndex[] FillLastMonth(this CurrencyIndex[] _array)
        {
            if (_array == null)
                return null;

            List<CurrencyIndex> _return = _array.ToList();
            CurrencyIndex _last = _return.OrderBy(_ob => _ob.Date).Last();
            int _lastday = DateTime.DaysInMonth(_last.Date.Year, _last.Date.Month);

            while (_last.Date.Day < _lastday)
            {
                _last = new CurrencyIndex
                {
                    ISO4217 = _last.ISO4217,
                    Date = _last.Date.AddDays(1),
                    Value = _last.Value
                };

                _return.Add(_last);
            }

            return _return.OrderBy(_ob => _ob.Date).ToArray();
        }
    }
}
