using System;

using imL.Enumeration;

namespace imL.Struct
{
    public struct DataInfo
    {
        public EDataBasicType Basic { get; }
        public EDataType Generic { get; }
        public Type Type { get; }

        public DataInfo(object _obj)
        {
            EDataType _g = EDataType.Unknown;

            if (_g == EDataType.Unknown && _obj is bool) _g = EDataType.Boolean;

            if (_g == EDataType.Unknown && _obj is sbyte) _g = EDataType.Integer;
            if (_g == EDataType.Unknown && _obj is byte) _g = EDataType.Integer;
            if (_g == EDataType.Unknown && _obj is short) _g = EDataType.Integer;
            if (_g == EDataType.Unknown && _obj is ushort) _g = EDataType.Integer;
            if (_g == EDataType.Unknown && _obj is int) _g = EDataType.Integer;
            if (_g == EDataType.Unknown && _obj is uint) _g = EDataType.Integer;
            if (_g == EDataType.Unknown && _obj is long) _g = EDataType.Integer;
            if (_g == EDataType.Unknown && _obj is ulong) _g = EDataType.Integer;
#if NET5_0_OR_GREATER
            if (_g == EDataType.Unknown && _obj is nint) _g = EDataType.Integer;
            if (_g == EDataType.Unknown && _obj is nuint) _g = EDataType.Integer;
#endif

            if (_g == EDataType.Unknown && _obj is float) _g = EDataType.Fraction;
            if (_g == EDataType.Unknown && _obj is double) _g = EDataType.Fraction;
            if (_g == EDataType.Unknown && _obj is decimal) _g = EDataType.Fraction;

#if NET6_0_OR_GREATER
            if (_g == EDataType.Unknown && _obj is TimeOnly) _g = EDataType.Time;
#endif
            if (_g == EDataType.Unknown && _obj is TimeSpan) _g = EDataType.Time;
#if NET6_0_OR_GREATER
            if (_g == EDataType.Unknown && _obj is DateOnly) _g = EDataType.Date;
#endif
            if (_g == EDataType.Unknown && _obj is DateTimeOffset) _g = EDataType.DateTime;
            if (_g == EDataType.Unknown && _obj is DateTime) _g = EDataType.DateTime;

            if (_g == EDataType.Unknown && _obj is string) _g = EDataType.String;

            this.Generic = _g;
            this.Basic = GetBasic(this.Generic);
            this.Type = this.Basic == EDataBasicType.Unknown ? null : _obj.GetType();
        }
        public DataInfo(string _code)
        {
            this.Type = null;

            switch (_code)
            {
                case "System.Boolean": this.Generic = EDataType.Boolean; break;
                case "System.SByte":
                case "System.Byte":
                case "System.Int16":
                case "System.UInt16":
                case "System.Int32":
                case "System.UInt32":
                case "System.Int64":
                case "System.UInt64":
                case "System.IntPtr":
                case "System.UIntPtr":
                    this.Generic = EDataType.Integer;

                    break;
                case "System.Single":
                case "System.Double":
                case "System.Decimal":
                    this.Generic = EDataType.Fraction;

                    break;
                case "System.TimeOnly":
                case "System.TimeSpan":
                    this.Generic = EDataType.Time;

                    break;
                case "System.DateOnly":
                    this.Generic = EDataType.Date;

                    break;
                case "System.DateTimeOffset":
                case "System.DateTime":
                    this.Generic = EDataType.DateTime;

                    break;
                case "System.String": this.Generic = EDataType.String; break;
                default: this.Generic = EDataType.Unknown; break;
            }

            this.Basic = GetBasic(this.Generic);
            this.Type = this.Basic == EDataBasicType.Unknown ? null : Type.GetType(_code);
        }

        internal static EDataBasicType GetBasic(EDataType _type)
        {
            switch (_type)
            {
                case EDataType.Boolean: return EDataBasicType.Boolean;
                case EDataType.Integer:
                case EDataType.Fraction:
                    return EDataBasicType.Number;
                case EDataType.Time:
                case EDataType.Date:
                case EDataType.DateTime:
                    return EDataBasicType.DateTime;
                case EDataType.String: return EDataBasicType.String;
                default: return EDataBasicType.Unknown;
            }
        }
    }
}


// var typeProcessorMap = new Dictionary<Type, Delegate>
//{
//    { typeof(int), new Action<int>(i => { /* do something with i */ }) },
//    { typeof(string), new Action<string>(s => { /* do something with s */ }) },
//};

//void ValidateProperties(object o)
//{
//    var t = o.GetType();
//    typeProcessorMap[t].DynamicInvoke(o); // invoke appropriate delegate
//}