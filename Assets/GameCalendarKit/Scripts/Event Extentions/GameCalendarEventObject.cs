using System;
using UnityEngine;

namespace GameCalendarKit
{
    ///  <para>
    ///    Dont forget that extend class must be in the same namespace with the original - GameTime.
    ///  </para>
    public partial class GameCalendarEventObject
    {
        public GameCalendarEventObject(DateTime dateStart, DateTime dateEnd, string title, string text, int daily, int weekly, int monthly, int yearly, bool byDate)
        {
            _dateStart = dateStart.ToString();
            _dateEnd = (dateEnd >= dateStart) ? dateEnd.ToString() : dateStart.ToString();
            _title = title;
            _text = text;

            Daily = daily;
            Weekly = weekly;
            Monthly = monthly;
            Yearly = yearly;

            RepeatByDate = byDate;
        }

        [SerializeField]
        string _text;


        public string Text
        {
            get
            {
                return _text;
            }
        }
    }
}

