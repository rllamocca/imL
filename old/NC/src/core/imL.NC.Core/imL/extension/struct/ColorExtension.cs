#if (NET35_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)

using System;
using System.Drawing;
using System.Linq;

namespace imL
{
    public static class ColorExtension
    {
        public static string ToStringRGB(this Color _this)
        {
            return string.Format("rgb({0},{1},{2})", _this.R, _this.G, _this.B);
        }
        public static string ToStringRGBA(this Color _this)
        {
            return string.Format("rgba({0},{1},{2},{3})", _this.R, _this.G, _this.B, Convert.ToString(Math.Round(_this.A / 255.0, 4), ReadOnly._CULTURE_INVARIANT));
        }
        public static Color Blend(this Color _this, Color _add, double _por = 0.5)
        {
            double _por2 = 1.0 - _por;

            double _a = _this.A * _por + _add.A * _por2;
            double _r = _this.R * _por + _add.R * _por2;
            double _g = _this.G * _por + _add.G * _por2;
            double _b = _this.B * _por + _add.B * _por2;

            return Color.FromArgb(Convert.ToByte(_a), Convert.ToByte(_r), Convert.ToByte(_g), Convert.ToByte(_b));
        }

        public static string[]
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP
        ?
#endif
            ToStringRGB(this Color[] _array)
        {
            if (_array == null)
                return null;

            return _array.Select(_s => _s.ToStringRGB()).ToArray();
        }
        public static string[] ToStringRGBA(this Color[] _array)
        {
            if (_array == null)
                return null;

            return _array.Select(_s => _s.ToStringRGBA()).ToArray();
        }
        public static Color Blend(this Color[] _array)
        {
            if (_array == null)
                return default;

            double _por = 1.0 / _array.Length;
            double _a = 0;
            double _r = 0;
            double _g = 0;
            double _b = 0;

            for (int _i = 0; _i < _array.Length; _i++)
            {
                Color _item = _array[_i];
                _a += _item.A;
                _r += _item.R;
                _g += _item.G;
                _b += _item.B;
            }

            _a = _por * _a;
            _r = _por * _r;
            _g = _por * _g;
            _b = _por * _b;

            return Color.FromArgb(Convert.ToByte(_a), Convert.ToByte(_r), Convert.ToByte(_g), Convert.ToByte(_b));
        }
        public static Color Blend_CMYK(this Color[] _array)
        {
            if (_array == null)
                return default;

            double _por = 1.0 / _array.Length;
            double _a = 0;
            double _r = 0;
            double _g = 0;
            double _b = 0;

            for (int _i = 0; _i < _array.Length; _i++)
            {
                Color _item = _array[_i];
                _a += _item.A;
                _r += _item.R;
                _g += _item.G;
                _b += _item.B;
            }

            _a = _por * _a;
            _r = _por * _r;
            _g = _por * _g;
            _b = _por * _b;

            return Color.FromArgb(Convert.ToByte(_a), Convert.ToByte(_r), Convert.ToByte(_g), Convert.ToByte(_b));
        }
    }
}

#endif