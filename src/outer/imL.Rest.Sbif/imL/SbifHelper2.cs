using System;
using System.Collections.Generic;
using System.Globalization;

using imL.Rest.Sbif.Schema;

namespace imL.Rest.Sbif
{
    public static class SbifHelper
    {
        static readonly CultureInfo _CULTURE = CultureInfo.GetCultureInfo("es-cl");
        static readonly string _ISO_4217 = (new RegionInfo(_CULTURE.LCID)).ISOCurrencySymbol;
        
        internal static EResource _RESOURCE = EResource.UF;

        public static IEnumerable<CurrencyIndex> Factory(IEnumerable<InternalIndex> _from)
        {
            foreach (InternalIndex _item in _from)
            {
                yield return new CurrencyIndex
                {
                    ISO4217 = _ISO_4217,
                    Date = Convert.ToDateTime(_item.Fecha, _CULTURE),
                    Value = Convert.ToDecimal(_item.Valor, _CULTURE)
                };
            }
        }
    }
}
