using System;
using System.Reflection;

namespace imL.Utility
{
    public static class GenericUtility
    {
        public static G ToAssign<G>(G _value, G _affect)
        {
            if (_value != null)
                return _value;

            return _affect;
        }
        public static void ToAssignRef<G>(G _value, ref G _affect)
        {
            if (_value != null)
                _affect = _value;
        }

        public static void ToAssignDelegate<G>(G _value, Action<G> _affect)
        {
            if (_value != null)
                _affect(_value);
        }

#if (NET45_OR_GREATER || NETSTANDARD1_0_OR_GREATER || NET5_0_OR_GREATER)
        public static G2 GetAttribute<G1, G2>()
        {
            TypeInfo _ti = typeof(G1).GetTypeInfo();
            Attribute _a = _ti.GetCustomAttribute(typeof(G2));

            if (_a == null)
                return default;

            if (_a is G2 _g)
                return _g;

            return default;
        }
#endif

    }
}
