#if (NET35_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)

using System.Timers;

namespace imL
{
    public sealed class ElapsedTime : ProgressAbstract
    {
        bool _DISPOSED = false;

        readonly Timer _TIMER;

        void ElapsedEvent(object _source, ElapsedEventArgs _e)
        {
            DrawElapsed(_e.SignalTime);
        }

        public ElapsedTime(ProgressAbstract _parent = null)
        {
            Init(_parent: _parent);
            Init2(0);

            DrawElapsed(_START);

            _TIMER = new Timer(1000);
            _TIMER.Elapsed += ElapsedEvent;
            _TIMER.Start();
        }

        //################################################################################
        protected override void Dispose(bool _managed)
        {
            if (_DISPOSED)
                return;

            if (_managed)
            {
                if (_TIMER != null)
                {
                    _TIMER.Stop();
                    _TIMER.Dispose();
                }
            }

            _DISPOSED = true;

            base.Dispose(_managed);
        }
    }
}

#endif