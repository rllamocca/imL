#if (NET35 || NET40)
using imL.Contract;
#endif

using System;

using imL.Contract.Terminal;
using imL.Enumeration;

namespace imL.Utility.Terminal.Fulfill
{
    public sealed class FProgress32 : AProgress
#if (NET35 || NET40)
        , IProgress<int>
#else
        , IProgress<int>
#endif
    {
        private bool _DISPOSED = false;

        private int _LENGTH;
        private int _VALUE = 0;

        public int Length { get { return this._LENGTH; } }
        public int Value { get { return this._VALUE; } }

        public FProgress32(int _length = 50, EReportProgress _report = EReportProgress.Increment, AProgress _parent = null)
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

        public void Report(int _value = 0)
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

        /*
        ~ProgressBar32() => Dispose(false);
        public new void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        */
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