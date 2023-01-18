#if (NET35_OR_GREATER || NETSTANDARD1_3_OR_GREATER || NET5_0_OR_GREATER)

#if (NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
//using imL.Struct;
#else
using System.Drawing;
#endif

using System;

namespace imL
{
    public static class ConsoleHelper
    {
        public static void Begins(bool _enc = true, string _title = null)
        {
            if (_enc)
            {
                Console.InputEncoding = ReadOnly._ENCODING;
                Console.OutputEncoding = ReadOnly._ENCODING;
            }

            if (_title.HasValue())
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
                Console.WriteLine(Environment.NewLine + @" ♫♪ ... {0}", _card.Value ? StringHelper.MyFortuneCard() : StringHelper.MyFortune());

            if (_rk)
                ConsoleHelper.PressAnyKeyToExit();
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

        public static void InnerException(Exception _ex)
        {
            Console.WriteLine(_ex);

            if (_ex.InnerException != null)
                InnerException(_ex.InnerException);
        }
    }
}

#endif