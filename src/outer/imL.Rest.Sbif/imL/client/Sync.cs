using System;
using System.Collections.Generic;

using imL.Rest.SBIF.Schema;

namespace imL.Rest.SBIF
{
    public partial class SBIFClient
    {
        public static IEnumerable<CurrencyIndex> FactoryISync(IEnumerable<InternalIndex> _from)
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
