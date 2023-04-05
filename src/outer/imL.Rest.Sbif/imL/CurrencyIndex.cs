using System;

namespace imL.Rest.SBIF
{
#if (NET5_0_OR_GREATER)
    public record CurrencyIndex
    {
        public string ISO4217 { init; get; }
        public DateTime Date { init; get; }
        public decimal Value { init; get; }
    }
#else
    public class CurrencyIndex
    {
        public string ISO4217 { set; get; }
        public DateTime Date { set; get; }
        public decimal Value { set; get; }
    }
#endif
}
