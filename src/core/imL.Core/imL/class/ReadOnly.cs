using System;
using System.Globalization;
using System.Text;

namespace imL
{
    public static class ReadOnly
    {
        public static readonly char[] _NUMBERS = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        public static readonly char[] _UPPERCASE = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'Ñ', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        public static readonly char[] _LOWERCASE = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'ñ', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        public static readonly char[] _SPECIALS = new char[] { '|', '°', '¬', '#', '$', '%', '&', '=', ',', ';', '.', ':', '¨', '*', '+', '~', '-', '_', '^',
            '`', '´',
            '(', ')',
            '[', ']',
            '{', '}',
            '<', '>',
            '¡', '!',
            '¿', '?',
            '/', '\\',
            '\'', '\"' };

        public static readonly DateTime _TIMESTAMP = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        //public static readonly DateTime _EXCEL_NTIME = new DateTime(1799, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc);
        public static readonly DateTime _EXCEL_TIME = new DateTime(1899, 12, 31);
        public static readonly DateTime _ANVIZ = new DateTime(2000, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc);

        public static readonly char[] _SUITS = new char[] { '♠', '♥', '♣', '♦', '☺', '☻' };
        public static readonly char[] _SQUAD = new char[] { 'A', '2', '3', '4', '5', '6', '7', '8', '9', 'X', 'J', 'Q', 'K' };
        public static readonly string? _CARD = @"
 ╔═════╗
 ║ {0}   ║
 ║  {1}  ║
 ║   {0} ║
 ╚═════╝
";
        public static readonly string? _JCARD = @"
 ╔═════╗
 ║  {0}  ║
 ║JOKER║
 ║  {0}  ║
 ╚═════╝
";
        public static readonly Encoding _ENCODING = Encoding.UTF8;
        public static readonly Encoding _ENCODING_NO_BOM = new UTF8Encoding(false);
        public static readonly CultureInfo _CULTURE_INVARIANT = CultureInfo.InvariantCulture;

        public static Encoding DefaultEncoding(string? _name)
        {
            if (_name == null)
                return _ENCODING;

            return Encoding.GetEncoding(_name);
        }

        public static void DefaultEncoding(ref Encoding _ref)
        {
            if (_ref == null)
                _ref = _ENCODING;
        }

        public static void DefaultEncoding_NoBOM(ref Encoding _ref)
        {
            if (_ref == null)
                _ref = _ENCODING_NO_BOM;
        }
        public static Encoding DefaultEncoding_NoBOM(Encoding? _ref)
        {
            if (_ref == null)
                return _ENCODING_NO_BOM;

            return _ref;
        }
    }
}
