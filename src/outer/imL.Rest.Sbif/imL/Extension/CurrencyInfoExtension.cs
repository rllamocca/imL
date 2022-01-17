using System;
using System.Collections.Generic;
using System.Linq;

namespace imL.Rest.Sbif
{
    public static class CurrencyInfoExtension
    {
        public static CurrencyInfo[] FillLastMonth(this CurrencyInfo[] _array)
        {
            if (_array == null)
                return null;

            List<CurrencyInfo> _return = _array.ToList();
            CurrencyInfo _last = _return.OrderBy(_ob => _ob.Date).Last();
            int _lastday = DateTime.DaysInMonth(_last.Date.Year, _last.Date.Month);

            while (_last.Date.Day < _lastday)
            {
                _last = new CurrencyInfo
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
