using System;

namespace GameCalendarKit.Clock
{
    public class GameClockEventObject : EventArgs
    {

        private readonly bool _status;
        private readonly int _hour;
        private readonly int _minute;
        private readonly int _second;

        public GameClockEventObject(bool status, int hour, int minute, int second)
        {
            _status = status;
            _hour = hour;
            _minute = minute;
            _second = second;
        }

        public bool Status
        {
            get
            {
                return _status;
            }
        }

        public int Hour
        {
            get
            {
                return _hour;
            }
        }

        public int Minute
        {
            get
            {
                return _minute;
            }
        }

        public int Second
        {
            get
            {
                return _second;
            }
        }
    }
}