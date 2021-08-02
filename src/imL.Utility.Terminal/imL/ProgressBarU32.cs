﻿using System;

namespace imL.Utility.Terminal
{
    public class ProgressBarU32 : ProgressBar
#if (NET35 || NET40)
        , Contract.IProgress<uint>
#else
        , IProgress<uint>
#endif
    {
        private bool _DISPOSED = false;

        private uint _LENGTH;
        private uint _VALUE = 0;

        public uint Length { get { return this._LENGTH; } }
        public uint Value { get { return this._VALUE; } }

        public ProgressBarU32(uint _length = 50, ProgressBar _parent = null)
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

        public void Report(uint _value = 0)
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

            string _text = string.Format("{0}  {1}/{2}",
                _per.ToString("P"),
                this._VALUE,
                this._LENGTH);

            ConsoleHelper.Write(this._DRAW_END, _text);
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