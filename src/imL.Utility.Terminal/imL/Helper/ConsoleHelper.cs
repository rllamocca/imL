#if (NETSTANDARD1_3)
using imL.Struct;
#else
using System.Drawing;
#endif

using System;
using System.Text;

namespace imL.Utility.Terminal
{
    public static class ConsoleHelper
    {
        public static void Starts(bool _enc = true, string _title = null)
        {
            if (_enc)
            {
                Console.InputEncoding = Encoding.UTF8;
                Console.OutputEncoding = Encoding.UTF8;
            }

            if (_title.HasValue())
                Console.Title = _title;

            Console.WriteLine(Environment.NewLine + @" Start the magic trick ... ♪♫ " + Environment.NewLine);
        }
        public static void PressAnyKeyToExit()
        {
            Console.WriteLine(@" (Press any key to exit) ");
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
                ConsoleHelper.PressAnyKeyToExit();

            Console.WriteLine();
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