using System;
using System.Collections.Generic;

namespace imL
{
    public static class DatetimeExtension
    {
        public static bool IsDateTime(this DateTime _this)
        {
            return (_this.Millisecond > 0 || _this.Second > 0 || _this.Minute > 0 || _this.Hour > 0);
        }
        public static bool Excel18991231(this DateTime _this)
        {
            return (_this.Year == 1899 && _this.Month == 12 && _this.Day == 31);
        }

        public static DateTime ToExcelTime(this DateTime _this)
        {
            TimeSpan _tmp = _this - ReadOnly._EXCEL_TIME;

            return new DateTime(_tmp.Ticks);
        }

        public static DateTime[] DaysBetween(this DateTime _this, DateTime _z, DayOfWeek _day = DayOfWeek.Sunday)
        {
            DateTime _a = _this;
            List<DateTime> _return = new List<DateTime>();

            while (_a <= _z)
            {
                if (_a.DayOfWeek == _day)
                {
                    _return.Add(_a);
                    _a = _a.AddDays(6);
                }
                else
                    _a = _a.AddDays(1);
            }

            return _return.ToArray();
        }
        public static double ToTimeStamp(this DateTime _this)
        {
            TimeSpan _diff = _this.ToUniversalTime() - ReadOnly._TIMESTAMP;
            return _diff.TotalSeconds;
        }

        public static bool Between(this DateTime _this, DateTime _a, DateTime _z, bool _inclusive = true)
        {
            return _inclusive ? _a <= _this && _this <= _z : _a < _this && _this < _z;
        }
    }
}
