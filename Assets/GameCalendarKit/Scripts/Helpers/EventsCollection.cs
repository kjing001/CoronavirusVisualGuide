using System.Collections.Generic;
using UnityEngine;

namespace GameCalendarKit.Helpers
{
    /// <summary>  
    ///  EventsCollection is the successor of ScriptableObject and contain a List of GameCalendarEventObject
    /// </summary> 
    public class EventsCollection : ScriptableObject
    {
        [SerializeField]
        public List<GameCalendarEventObject> Events = new List<GameCalendarEventObject>();

    }
}