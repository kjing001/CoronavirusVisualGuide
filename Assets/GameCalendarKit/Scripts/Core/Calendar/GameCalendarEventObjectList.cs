using System;
using System.Collections.Generic;

namespace GameCalendarKit
{
    public class GameCalendarEventObjectList : EventArgs
    {
        public GameCalendarEventObjectList(List<GameCalendarEventObject> events)
        {
            _events = events;
        }

        readonly List<GameCalendarEventObject> _events;

        public List<GameCalendarEventObject> Events
        {
            get
            {
                return _events;
            }
        }
    }
}
