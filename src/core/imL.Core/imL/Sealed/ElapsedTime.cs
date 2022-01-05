#if (NET35_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)

using System.Timers;

using imL.Contract;

namespace imL.Sealed
{
    public sealed class ElapsedTime : ProgressAbstract
    {
        private bool _DISPOSED = false;

        private readonly Timer _TIMER;

        private void ElapsedEvent(object _source, ElapsedEventArgs _e)
        {
            this.DrawElapsed(_e.SignalTime);
        }

        public ElapsedTime(ProgressAbstract _parent = null)
        {
            this.Init(_parent: _parent);
            this.Init2(0);

            this.DrawElapsed(this._START);

            this._TIMER = new Timer(1000);
            this._TIMER.Elapsed += this.ElapsedEvent;
            this._TIMER.Start();
        }

        //################################################################################
        protected override void Dispose(bool _managed)
        {
            if (this._DISPOSED)
                return;

            if (_managed)
            {
                if (this._TIMER != null)
                {
                    this._TIMER.Stop();
                    this._TIMER.Dispose();
                }
            }

            this._DISPOSED = true;

            base.Dispose(_managed);
        }
    }
}

#endif