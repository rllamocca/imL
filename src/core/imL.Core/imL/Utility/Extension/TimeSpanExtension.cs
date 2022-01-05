using System;

namespace imL.Utility
{
    public static class TimeSpanExtension
    {
        public static DateTime ToExcelTime(this TimeSpan _this)
        {
            return new DateTime(1899, 12, 31).AddTicks(_this.Ticks);
        }
    }
}
