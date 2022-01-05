using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace TEST.SomeProof
{
    class Program
    {
        static void Main(
            //string[] args
            )
        {
            Console.WriteLine("Hello World!");

            Console.OutputEncoding = Encoding.UTF8;


            try
            {
                int thirtiethFib = Fibonacci().Skip(7).Take(1).First();

                Console.WriteLine(thirtiethFib);

                IEnumerable<int> _andsheesaid = null;
                if (_andsheesaid.Any(_w => _w == 888))
                    Console.WriteLine("_andsheesaid");
            }
            catch (Exception _ex)
            {
                Console.WriteLine(_ex);
            }


            try
            {
                IEnumerable<string> _currencySymbols = CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                    .OrderBy(_ob => _ob.Name)
                    .OrderBy(_ob => _ob.LCID)
                    .Select(_s =>
                    {
                        RegionInfo _ri = new(_s.LCID);
                        string _return = string.Format("{0} {1} {2} {3} {4} | {5} {6} {7} {8}",
                            _ri.ISOCurrencySymbol,
                            _ri.GeoId,
                            _ri.CurrencyNativeName,
                            _ri.CurrencyEnglishName,
                            _ri.CurrencySymbol,
                            _s.TwoLetterISOLanguageName,
                            _s.ThreeLetterISOLanguageName,
                            _s.ThreeLetterWindowsLanguageName,
                            _s.Name
                            );

                        return _return;
                    })
                    .Distinct()
                    .OrderBy(_ob => _ob);

                foreach (var _item in _currencySymbols)
                {
                    Console.WriteLine(_item);
                }
            }
            catch (Exception _ex)
            {
                Console.WriteLine(_ex);
            }

            Console.ReadKey();
        }

        static IEnumerable<int> Fibonacci()
        {
            int _a = 0, _b = 1;

            yield return _a;
            yield return _b;

            for (int _i = 3; _i < 47; _i++)
            {
                int _return = _a + _b;

                yield return _return;

                _a = _b;
                _b = _return;
            }
        }
    }
}
