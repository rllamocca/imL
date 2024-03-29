﻿#if (NET35_OR_GREATER || NETSTANDARD1_3_OR_GREATER || NET5_0_OR_GREATER)


using System;

namespace imL
{
    public sealed class ProgressU64 : ProgressAbstract
#if (NET35 || NET40)
        , IProgress<ulong>
#else
        , IProgress<ulong>
#endif
    {
        bool _DISPOSED = false;

        ulong _LENGTH;
        ulong _VALUE = 0;

        public ulong Length { get { return _LENGTH; } }
        public ulong Value { get { return _VALUE; } }

        public ProgressU64(ulong _length = 50, EReportProgress _report = EReportProgress.Increment, ProgressAbstract _parent = null)
        {
            if (_length == 0)
                throw new ArgumentOutOfRangeException(nameof(_length), "_length == 0");

            Init(_report, _parent);
            _LENGTH = _length;

            if (_LENGTH < 50)
                Init2(Convert.ToByte(_LENGTH));
            else
                Init2();
        }

        public void Report(ulong _value = 0)
        {
            switch (_REPORT)
            {
                case EReportProgress.StartsAtZero:
                    _VALUE = _value + 1;
                    break;
                case EReportProgress.Increment:
                    _VALUE++;
                    break;
                default:
                    _VALUE = _value;
                    break;
            }

            decimal _per = 1.0m;

            if (_VALUE.Between(0, _LENGTH))
            {
                _per = (1.0m * _VALUE / _LENGTH);
                DrawBar(_per);
            }

            DrawProgress(_per, _VALUE, _LENGTH);
        }

        //################################################################################
        protected override void Dispose(bool _managed)
        {
            if (_DISPOSED)
                return;

            if (_managed)
            {
                _LENGTH = 0;
                _VALUE = 0;
            }
            _DISPOSED = true;

            base.Dispose(_managed);
        }
    }
}

#endif