#if (NET35_OR_GREATER || NETSTANDARD1_3_OR_GREATER || NET5_0_OR_GREATER)

#if (NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
using imL.Struct;
#else
using System.Drawing;
#endif

using System;

namespace imL.Utility
{
    public static class TerminalHelper
    {
        public static void Starts(bool _enc = true, string _title = null)
        {
            if (_enc)
            {
                Console.InputEncoding = ReadOnly._ENCODING;
                Console.OutputEncoding = ReadOnly._ENCODING;
            }

            if (_title.HasValue())
                Console.Title = _title;

            Console.WriteLine(Environment.NewLine + @" Start the magic trick ... ♪♫ " + Environment.NewLine);
        }
        public static void PressAnyKeyToExit()
        {
            Console.WriteLine(Environment.NewLine + @" (Press any key to exit) ");
            Console.ReadKey();
        }
        public static void Ends(bool _rk = false, bool _card = true)
        {
            Console.WriteLine();

            if (_card)
                Console.WriteLine(@" ♫♪ ... {0}", StringHelper.MyFortuneCard());
            else
                Console.WriteLine(@" ♫♪ ... {0}", StringHelper.MyFortune());

            if (_rk)
                TerminalHelper.PressAnyKeyToExit();
        }

        public static void Write(Point _xy, char _value)
        {
            Console.SetCursorPosition(_xy.X, _xy.Y);
            Console.Write(_value);
        }
        public static void Write(Point _xy, string _value)
        {
            Console.SetCursorPosition(_xy.X, _xy.Y);
            Console.Write(_value);
        }
        public static void WriteLine(Point _xy, char _value)
        {
            Console.SetCursorPosition(_xy.X, _xy.Y);
            Console.WriteLine(_value);
        }
        public static void WriteLine(Point _xy, string _value)
        {
            Console.SetCursorPosition(_xy.X, _xy.Y);
            Console.WriteLine(_value);
        }
    }
}

#endif