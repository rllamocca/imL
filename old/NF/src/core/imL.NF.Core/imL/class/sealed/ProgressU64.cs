#if (NET35_OR_GREATER || NETSTANDARD1_3_OR_GREATER || NET5_0_OR_GREATER)

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

        public ulong Length { get { return this._LENGTH; } }
        public ulong Value { get { return this._VALUE; } }

        public ProgressU64(ulong _length = 50, EReportProgress _report = EReportProgress.Increment, ProgressAbstract _parent = null)
        {
            if (_length == 0)
                throw new ArgumentOutOfRangeException(nameof(_length), "_length == 0");

            this.Init(_report, _parent);
            this._LENGTH = _length;

            if (this._LENGTH < 50)
                this.Init2(Convert.ToByte(this._LENGTH));
            else
                this.Init2();
        }

        public void Report(ulong _value = 0)
        {
            switch (this._REPORT)
            {
                case EReportProgress.StartsAtZero:
                    this._VALUE = _value + 1;
                    break;
                case EReportProgress.Increment:
                    this._VALUE++;
                    break;
                default:
                    this._VALUE = _value;
                    break;
            }

            decimal _per = 1.0m;

            if (this._VALUE.Between(0, this._LENGTH))
            {
                _per = (1.0m * this._VALUE / this._LENGTH);
                this.DrawBar(_per);
            }

            this.DrawProgress(_per, this._VALUE, this._LENGTH);
        }

        //################################################################################
        protected override void Dispose(bool _managed)
        {
            if (this._DISPOSED)
                return;

            if (_managed)
            {
                this._LENGTH = 0;
                this._VALUE = 0;
            }
            this._DISPOSED = true;

            base.Dispose(_managed);
        }
    }
}

#endif