#if (NET35_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)

using System.Data;
using System.Reflection;
//using System.Security.Policy;


namespace imL
{
    public class Setter<G>
    {
        PropertyInfo[] _PROPS;
        string[] _KEYS;

        internal G I__CREATE__()
        {
            I__PROP__();
            return Activator.CreateInstance<G>();
        }
        //T FactoryInstance() where T : new()
        //{
        //    Init<T>();
        //    return new T();
        //}
        internal void I__PROP__()
        {
            if (_PROPS == null)
                _PROPS = typeof(G).GetProperties();
        }
        internal void I__KEY__(DataRow _dr)
        {
            if (_KEYS == null)
            {
                IList<string> _tmp = new List<string>();

                foreach (DataColumn _item in _dr.Table.Columns)
                    _tmp.Add(_item.ColumnName);

                _KEYS = _tmp.ToArray();
            }
        }
        internal void I__SET__(G _g, int _index, object _value)
        {
            PropertyInfo _prop = _PROPS[_index];
            I__VALUE__(_g, _prop, _value);
        }
        internal void I__SET__(G _g, string _name, object _value)
        {
            PropertyInfo _prop = _PROPS.Where(_w => _w.Name == _name).FirstOrDefault();
            I__VALUE__(_g, _prop, _value);
        }
        internal static void I__VALUE__(G _g, PropertyInfo _pi, object _value)
        {
            if (_pi.HasValue() && _pi.CanWrite)
            {
                if (_value.HasValue())
                    _pi.SetValue(_g, _value, null);
            }
        }

        public G Instance(params object[] _values)
        {
            if (_values == null)
                return default;

            G _return = I__CREATE__();

            for (int _i = 0; _i < _values.Length; _i++)
                I__SET__(_return, _i, _values[_i]);

            return _return;
        }
        public G Instance(params KeyValuePair<string, object>[] _values)
        {
            if (_values == null)
                return default;

            G _return = I__CREATE__();

            foreach (KeyValuePair<string, object> _item in _values)
                I__SET__(_return, _item.Key, _item.Value);

            return _return;
        }
        public G Instance(DataRow _values, bool _byindex = false)
        {
            if (_byindex)
                return Instance(_values.ItemArray);

            if (_values == null)
                return default;

            I__KEY__(_values);
            G _return = I__CREATE__();

            foreach (string _item in _KEYS)
                I__SET__(_return, _item, _values[_item]);

            return _return;
        }
    }
}

#endif