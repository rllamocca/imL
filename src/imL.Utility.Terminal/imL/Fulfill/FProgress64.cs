#if (NET35 || NET40)
using imL.Contract;
#endif

using System;

using imL.Contract.Terminal;
using imL.Enumeration;

namespace imL.Utility.Terminal.Fulfill
{
    public sealed class FProgress64 : AProgress
#if (NET35 || NET40)
        , IProgress<long>
#else
        , IProgress<long>
#endif
    {
        private bool _DISPOSED = false;

        private long _LENGTH;
        private long _VALUE = 0;

        public long Length { get { return this._LENGTH; } }
        public long Value { get { return this._VALUE; } }

        public FProgress64(long _length = 50, EReportProgress _report = EReportProgress.Increment, AProgress _parent = null)
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

        public void Report(long _value = 0)
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