using System;
using System.Collections.Generic;
using System.Globalization;

using imL.Rest.Sbif.Schema;

namespace imL.Rest.Sbif
{
    public static class SbifHelper
    {
        internal static readonly CultureInfo _CULTURE = CultureInfo.GetCultureInfo("es-cl");
        internal static readonly string _ISO_4217 = (new RegionInfo(SbifHelper._CULTURE.LCID)).ISOCurrencySymbol;
        internal static EResource _RESOURCE = EResource.UF;

        public static CurrencyIndex[] Factory(InternalIndex[] _from)
        {
            List<CurrencyIndex> _return = new List<CurrencyIndex>();

            foreach (InternalIndex _item in _from)
            {
                CurrencyIndex _new = new CurrencyIndex
                {
                    ISO4217 = SbifHelper._ISO_4217,
                    Date = Convert.ToDateTime(_item.Fecha, SbifHelper._CULTURE),
                    Value = Convert.ToDecimal(_item.Valor, SbifHelper._CULTURE)
                };

                _return.Add(_new);
            }

            return _return.ToArray();
        }
    }
}
