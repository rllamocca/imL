using System;

using imL.Enumeration;

namespace imL.Struct
{
    public readonly struct MemoryUnit
    {
        public decimal Size { get; }
        public EMemoryUnit Unit { get; }

        public MemoryUnit(decimal _size, EMemoryUnit _unit = EMemoryUnit.Byte)
        {
            Size = _size;
            Unit = _unit;
        }

        public MemoryUnit To(EMemoryUnit _new = EMemoryUnit.MB)
        {
            return new MemoryUnit(ToSize(_new), _new);
        }
        public decimal ToSize(EMemoryUnit _new = EMemoryUnit.MB)
        {
            if (Unit == _new || Size == 0)
                return Size;

            bool _cen = Unit < _new;
            double _diff;

            if (_cen)
                _diff = _new - Unit;
            else
                _diff = Unit - _new;

            _diff = Math.Pow(2, _diff);

            if (_cen)
                return Size / Convert.ToDecimal(_diff);
            else
                return Size * Convert.ToDecimal(_diff);
        }

        public static bool operator <(MemoryUnit _l, MemoryUnit _r)
        {
            if (_l.Unit == _r.Unit)
                return _l.Size < _r.Size;

            if (_l.Unit < _r.Unit)
                return _l.To(_r.Unit).Size < _r.Size;
            else
                return _l.Size < _r.To(_l.Unit).Size;
        }
        public static bool operator <=(MemoryUnit _l, MemoryUnit _r)
        {
            if (_l.Unit == _r.Unit)
                return _l.Size <= _r.Size;

            if (_l.Unit < _r.Unit)
                return _l.To(_r.Unit).Size <= _r.Size;
            else
                return _l.Size <= _r.To(_l.Unit).Size;
        }
        public static bool operator ==(MemoryUnit _l, MemoryUnit _r)
        {
            if (_l.Unit == _r.Unit)
                return _l.Size == _r.Size;

            if (_l.Unit < _r.Unit)
                return _l.To(_r.Unit).Size == _r.Size;
            else
                return _l.Size == _r.To(_l.Unit).Size;
        }
        public static bool operator !=(MemoryUnit _l, MemoryUnit _r)
        {
            return (_l == _r) == false;
        }
        public static bool operator >(MemoryUnit _l, MemoryUnit _r)
        {
            if (_l.Unit == _r.Unit)
                return _l.Size > _r.Size;

            if (_l.Unit < _r.Unit)
                return _l.To(_r.Unit).Size > _r.Size;
            else
                return _l.Size > _r.To(_l.Unit).Size;
        }
        public static bool operator >=(MemoryUnit _l, MemoryUnit _r)
        {
            if (_l.Unit == _r.Unit)
                return _l.Size >= _r.Size;

            if (_l.Unit < _r.Unit)
                return _l.To(_r.Unit).Size >= _r.Size;
            else
                return _l.Size >= _r.To(_l.Unit).Size;
        }

        public static MemoryUnit operator +(MemoryUnit _a)
        {
            return _a;
        }
        public static MemoryUnit operator -(MemoryUnit _a)
        {
            return new MemoryUnit(-_a.Size, _a.Unit);
        }
        public static MemoryUnit operator +(MemoryUnit _l, MemoryUnit _r)
        {
            if (_l.Unit == _r.Unit)
                return new MemoryUnit(_l.Size + _r.Size, _l.Unit);

            if (_l.Unit < _r.Unit)
                return new MemoryUnit(_l.Size + _r.To(_l.Unit).Size, _l.Unit);
            else
                return new MemoryUnit(_l.To(_r.Unit).Size + _r.Size, _r.Unit);
        }
        public static MemoryUnit operator -(MemoryUnit _l, MemoryUnit _r)
        {
            if (_l.Unit == _r.Unit)
                return new MemoryUnit(_l.Size + _r.Size, _l.Unit);

            if (_l.Unit < _r.Unit)
                return new MemoryUnit(_l.Size - _r.To(_l.Unit).Size, _l.Unit);
            else
                return new MemoryUnit(_l.To(_r.Unit).Size - _r.Size, _r.Unit);
        }

        public override bool Equals(object _obj)
        {
            if (_obj is MemoryUnit _mu)
                return this == _mu;

            return false;
        }
        public override string ToString()
        {
            return string.Format("{0} {1}s", Size, Unit);
        }
        public override int GetHashCode()
        {
            int _a = Convert.ToInt32(Size);
            int _b = Convert.ToInt32(Unit);

            return _a ^ _b;
        }
    }
}
