#if (NET35)

namespace imL
{
    public readonly struct Tuple<T1>
    {
        public T1 Item1 { get; }

        public Tuple(T1 item1)
        {
            Item1 = item1;
        }

        public override bool Equals(object obj)
        {
            throw new System.NotImplementedException();
        }

        public override int GetHashCode()
        {
            throw new System.NotImplementedException();
        }

        public static bool operator ==(Tuple<T1> left, Tuple<T1> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Tuple<T1> left, Tuple<T1> right)
        {
            return !(left == right);
        }
    }

    public readonly struct Tuple<T1, T2>
    {
        public T1 Item1 { get; }
        public T2 Item2 { get; }

        public Tuple(T1 item1, T2 item2)
        {
            Item1 = item1;
            Item2 = item2;
        }

        public override bool Equals(object obj)
        {
            throw new System.NotImplementedException();
        }

        public override int GetHashCode()
        {
            throw new System.NotImplementedException();
        }

        public static bool operator ==(Tuple<T1, T2> left, Tuple<T1, T2> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Tuple<T1, T2> left, Tuple<T1, T2> right)
        {
            return !(left == right);
        }
    }

    public readonly struct Tuple<T1, T2, T3>
    {
        public T1 Item1 { get; }
        public T2 Item2 { get; }
        public T3 Item3 { get; }

        public Tuple(T1 item1, T2 item2, T3 item3)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
        }

        public override bool Equals(object obj)
        {
            throw new System.NotImplementedException();
        }

        public override int GetHashCode()
        {
            throw new System.NotImplementedException();
        }

        public static bool operator ==(Tuple<T1, T2, T3> left, Tuple<T1, T2, T3> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Tuple<T1, T2, T3> left, Tuple<T1, T2, T3> right)
        {
            return !(left == right);
        }
    }
}

#endif