#if (NET35_OR_GREATER || NETSTANDARD1_3_OR_GREATER || NET5_0_OR_GREATER)

#if (NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
//using imL.Struct;
#else
#endif

using System;
using System.Text;
//using System.Diagnostics.Contracts;

namespace imL
{
    public static partial class ConsoleHelper
    {
        public static void Begins(bool _enc = true, string? _title = null)
        {
            if (_enc)
            {
                Console.InputEncoding = Encoding.UTF8;
                Console.OutputEncoding = Console.InputEncoding;
            }

            if (_title != null)
                Console.Title = _title;

            Console.WriteLine(Environment.NewLine + @" Start the magic trick ... ♪♫ ");
        }
        public static void PressAnyKeyToExit()
        {
            Console.WriteLine(Environment.NewLine + @" (Press any key to exit) ");
            Console.ReadKey();
        }
        public static void Ends(bool _rk = false, bool? _card = null)
        {
            if (_card.HasValue)
                Console.WriteLine(Environment.NewLine + @" ♫♪ ... {0}", _card.Value ? RichieHelper.MyFortuneCard() : RichieHelper.MyFortune());

            if (_rk)
                PressAnyKeyToExit();
        }
    }
}

#endif