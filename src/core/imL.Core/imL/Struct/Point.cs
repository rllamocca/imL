#if (NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)

using System;

namespace imL.Struct
{
    public struct Point
    {
        public int X { set; get; }
        public int Y { set; get; }

        public Point(int _x, int _y)
        {
            X = _x;
            Y = _y;
        }

        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(Point left, Point right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Point left, Point right)
        {
            return !(left == right);
        }
    }
}

#endif