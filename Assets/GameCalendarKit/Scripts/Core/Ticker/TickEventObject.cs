using System;

namespace GameCalendarKit.TickerUtil
{
    public class TickEventObject : EventArgs
    {
        private readonly bool _status;
        private readonly int _currentTick;

        public TickEventObject(bool status, int currentTick)
        {
            _status = status;
            _currentTick = currentTick;
        }

        public bool Status
        {
            get
            {
                return _status;
            }
        }

        public int CurrentTick
        {
            get
            {
                return _currentTick;
            }
        }
    }
}

