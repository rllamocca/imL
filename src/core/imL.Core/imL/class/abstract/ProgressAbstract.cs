﻿#if (NET35_OR_GREATER || NETSTANDARD1_3_OR_GREATER || NET5_0_OR_GREATER)

#if (NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
//using imL.Struct;
#else
using System;
using System.Collections.Generic;
using System.Drawing;
#endif

using System.Drawing;
using System;
using System.Collections.Generic;

namespace imL
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
        IList<decimal> _BAR = new List<decimal>() { 0 };

        public DateTime Start { get { return _START; } }

        protected void Init(EReportProgress _report = EReportProgress.None, ProgressAbstract _parent = null)
        {
            _START = DateTime.Now;
            _PARENT = _parent;
            _REPORT = _report;
        }
        protected void Init2(byte _blocks = 50)
        {
            if (_PARENT == null)
            {
                _LINE = new Point(Console.CursorLeft, Console.CursorTop);
                Console.CursorVisible = false;
            }
            else
                _LINE = new Point(_PARENT._LINE.X + 2, _PARENT._LINE.Y + 1);

            _DRAW_START = new Point(_LINE.X, _LINE.Y);

            if (_blocks < _BLOCKS)
                _BLOCKS = _blocks;

            if (_BLOCKS > 0)
            {
                _DRAW_END = new Point(_DRAW_START.X + _BLOCKS + 4, _DRAW_START.Y);

                ConsoleHelper.Write(_DRAW_START, string.Format("[{0}]{1}", new string(' ', _BLOCKS), new string(' ', 75)));
                _DRAW_START.X += 1;
            }

            _NEW_LINE = new Point(_LINE.X, _LINE.Y + 1);
        }
        protected void DrawBar(decimal _per)
        {
            if (_per.Between(0, 1, EInterval.Until))
            {
                decimal _round = Math.Round(_per * _BLOCKS);

                if (_BAR.Contains(_round) == false)
                {
                    _BAR.Add(_round);

                    ConsoleHelper.Write(_DRAW_START, _CHAR);
                    _DRAW_START.X += 1;
                }
            }
        }
        protected void DrawProgress(decimal _per, object _value, object _length)
        {
            string _text = string.Format("{0}  {1} /{2}",
                _per.ToString("P"),
                _value,
                _length);

            ConsoleHelper.Write(_DRAW_END, _text);
        }
        protected void DrawElapsed(DateTime _value)
        {
            TimeSpan _diff = _value - _START;

            string _text = string.Format(
                "[{0}]  {1}",
                _BUCLE[_BLOCKS++ % 4],
#if (NET35)
                _diff.ToString()
#else
                _diff.ToString("hh':'mm':'ss")
#endif
                );

            ConsoleHelper.Write(_DRAW_START, _text);
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
            if (_DISPOSED)
                return;

            if (_managed)
            {
                if (_PARENT == null)
                {
                    Console.CursorVisible = true;
                    ConsoleHelper.WriteLine(_NEW_LINE, ' ');
                }
                else
                    _PARENT._NEW_LINE = new Point(_NEW_LINE.X, _NEW_LINE.Y);

                _DRAW_END = new Point(0, 0);
                _START = DateTime.MinValue;

                _BLOCKS = 0;
                _LINE = new Point(0, 0);
                _NEW_LINE = new Point(0, 0);
                _DRAW_START = new Point(0, 0);
                _BAR.Clear();
                _BAR = null;
            }
            _DISPOSED = true;
        }
    }
}

#endif