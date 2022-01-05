#if (NET35 || NET40)

namespace imL.Contract
{
    public interface IProgress<in T>
    {
        void Report(T _value);
    }

}

#endif