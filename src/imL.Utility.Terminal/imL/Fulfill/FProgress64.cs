using System;

using imL.Contract.Terminal;

namespace imL.Utility.Terminal.Fulfill
{
    public sealed class FProgress64 : AProgress
#if (NET35 || NET40)
        , Contract.IProgress<long>
#else
        , IProgress<long>
#endif
    {
        private bool _DISPOSED = false;

        private long _LENGTH;
        private long _VALUE = 0;

        public long Length { get { return this._LENGTH; } }
        public long Value { get { return this._VALUE; } }

        public FProgress64(long _length = 50, AProgress _parent = null)
        {
            this._LENGTH = _length;
            this._PARENT = _parent;

            if (this._LENGTH == 0)
                throw new ArgumentOutOfRangeException(nameof(_length), "_length == 0");

            if (this._LENGTH < 50)
                this.Init(Convert.ToByte(this._LENGTH));
            else
                this.Init();
        }

        public void Report(long _value = 0)
        {
            if (_value == 0)
                this._VALUE++;
            else
                this._VALUE = _value;

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