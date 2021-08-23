using System;
using System.Globalization;

namespace TEST.SomeProof
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            decimal _lat = 123456789.9876543210m;

            //NumberFormatInfo _nfi = new();
            //_nfi.NumberDecimalSeparator = ".";

            //Console.WriteLine(_lat);
            //Console.WriteLine(Convert.ToString(_lat, CultureInfo.GetCultureInfo("en-US")));
            //Console.WriteLine(Convert.ToString(_lat, CultureInfo.GetCultureInfo("es-CL")));

            //Console.WriteLine(Convert.ToString(_lat, _nfi));

            //Console.WriteLine(_lat.ToString("c"));
            ////Console.WriteLine(_lat.ToString("d"));
            //Console.WriteLine(_lat.ToString("e"));
            //Console.WriteLine(_lat.ToString("f"));
            Console.WriteLine(_lat.ToString("g"));
            Console.WriteLine(_lat.ToString("g", CultureInfo.GetCultureInfo("es-CL")));
            //Console.WriteLine(_lat.ToString("n"));
            //Console.WriteLine(_lat.ToString("p"));
            //Console.WriteLine(_lat.ToString("r"));
            //Console.WriteLine(_lat.ToString("x"));

            bool _bool = test(true);

            Console.ReadKey();
        }

        static bool test(bool _throw = false, params string[] _lala)
        {
            return false;
        }
    }
}
