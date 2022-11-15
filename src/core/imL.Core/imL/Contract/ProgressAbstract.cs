#if (NET35_OR_GREATER || NETSTANDARD1_3_OR_GREATER || NET5_0_OR_GREATER)

#if (NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
using imL.Struct;
#else
using System.Drawing;
#endif

using System;
using System.Collections.Generic;

using imL.Enumeration;
using imL.Utility;

namespace imL.Contract
{
    public abstract class ProgressAbstract : IDisposable
    {
        bool _DISPOSED = false;

        protected ProgressAbstract _PARENT;
        protected EReportProgress _REPORT;
        protected Point _DRAW_END;
        protected DateTime _START;

        const char _CHAR = '■';
        const string _BUCLE = @"+\|/";
        byte _BLOCKS = 50;
        Point _LINE;
        Point _NEW_LINE;
        Point _DRAW_START;
        List<decimal> _BAR = new List<decimal>() { 0 };

        public DateTime Start { get { return this._START; } }

        protected void Init(EReportProgress _report = EReportProgress.None, ProgressAbstract _parent = null)
        {
            this._START = DateTime.Now;
            this._PARENT = _parent;
            this._REPORT = _report;
        }
        protected void Init2(byte _blocks = 50)
        {
            if (this._PARENT == null)
            {
                this._LINE = new Point(Console.CursorLeft, Console.CursorTop);
                Console.CursorVisible = false;
            }
            else
                this._LINE = new Point(this._PARENT._LINE.X + 2, this._PARENT._LINE.Y + 1);

            this._DRAW_START = new Point(this._LINE.X, this._LINE.Y);

            if (_blocks < this._BLOCKS)
                this._BLOCKS = _blocks;

            if (this._BLOCKS > 0)
            {
                this._DRAW_END = new Point(this._DRAW_START.X + this._BLOCKS + 4, this._DRAW_START.Y);

                ConsoleHelper.Write(this._DRAW_START, string.Format("[{0}]{1}", new string(' ', this._BLOCKS), new string(' ', 75)));
                this._DRAW_START.X += 1;
            }

            this._NEW_LINE = new Point(this._LINE.X, this._LINE.Y + 1);
        }
        protected void DrawBar(decimal _per)
        {
            if (_per.Between(0, 1, EInterval.Until))
            {
                decimal _round = Math.Round(_per * this._BLOCKS);

                if (this._BAR.Contains(_round) == false)
                {
                    this._BAR.Add(_round);

                    ConsoleHelper.Write(this._DRAW_START, ProgressAbstract._CHAR);
                    this._DRAW_START.X += 1;
                }
            }
        }
        protected void DrawProgress(decimal _per, object _value, object _length)
        {
            string _text = string.Format("{0}  {1} /{2}",
                _per.ToString("P"),
                _value,
                _length);

            ConsoleHelper.Write(this._DRAW_END, _text);
        }
        protected void DrawElapsed(DateTime _value)
        {
            TimeSpan _diff = _value - this._START;

            string _text = string.Format(
                "[{0}]  {1}",
                ProgressAbstract._BUCLE[this._BLOCKS++ % 4],
#if (NET35)
                _diff.ToString()
#else
                _diff.ToString("hh':'mm':'ss")
#endif
                );

            ConsoleHelper.Write(this._DRAW_START, _text);
        }

        //################################################################################
        ~ProgressAbstract() => Dispose(false);
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool _managed)
        {
            if (this._DISPOSED)
                return;

            if (_managed)
            {
                if (this._PARENT == null)
                {
                    Console.CursorVisible = true;
                    ConsoleHelper.WriteLine(this._NEW_LINE, ' ');
                }
                else
                    this._PARENT._NEW_LINE = new Point(this._NEW_LINE.X, this._NEW_LINE.Y);

                this._DRAW_END = new Point(0, 0);
                this._START = DateTime.MinValue;

                this._BLOCKS = 0;
                this._LINE = new Point(0, 0);
                this._NEW_LINE = new Point(0, 0);
                this._DRAW_START = new Point(0, 0);
                this._BAR.Clear();
                this._BAR = null;
            }
            this._DISPOSED = true;
        }
    }
}

#endif