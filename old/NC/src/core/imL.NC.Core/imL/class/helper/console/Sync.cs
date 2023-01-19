#if (NET35_OR_GREATER || NETSTANDARD1_3_OR_GREATER || NET5_0_OR_GREATER)

#if (NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
//using imL.Struct;
#else
using System.Drawing;
#endif

using System;
using System.Diagnostics.Contracts;

namespace imL
{
    public static partial class ConsoleHelper
    {
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
        public static void WriteInnerException(Exception _ex)
        {
            foreach (Exception _item in _ex.InnerExceptionISync())
                Console.WriteLine(_item);
        }
    }
}

#endif