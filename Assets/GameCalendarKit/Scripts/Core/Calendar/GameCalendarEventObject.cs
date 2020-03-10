using System;
using UnityEngine;

namespace GameCalendarKit
{
    /// <summary>  
    ///  GameCalendarEventObject is the successor of EventArgs and have a partial keyword.
    ///  <para>
    ///    partial keyword give you a possibility to extend current class and create your own event object.
    ///  </para>
    ///  <para>
    ///    more about partial is here - https://msdn.microsoft.com/en-us//library/wa80x488.aspx
    ///  </para>
    /// </summary> 
    [Serializable]
    public partial class GameCalendarEventObject : EventArgs
    {

        public GameCalendarEventObject(DateTime dateStart, DateTime dateEnd, string title)
        {
            _dateStart = dateStart.ToString();
            _dateEnd = (dateEnd>= dateStart)? dateEnd.ToString() : dateStart.ToString();
            _title = title;
        }

        public GameCalendarEventObject(DateTime dateStart, DateTime dateEnd, string title, int daily, int weekly, int monthly, int yearly, bool byDate)
        {
            _dateStart = dateStart.ToString();
            _dateEnd = (dateEnd >= dateStart) ? dateEnd.ToString() : dateStart.ToString();
            _title = title;

            Daily = daily;
            Weekly = weekly;
            Monthly = monthly;
            Yearly = yearly;

            RepeatByDate = byDate;
        }

        [SerializeField]
        string _dateStart;
        DateTime _tempDateStart;
        [SerializeField]
        string _dateEnd;
        DateTime _tempDateEnd;
        [SerializeField]
        string _title;

        [SerializeField]
        int _daily = 0;
        [SerializeField]
        int _weekly = 0;
        [SerializeField]
        int _monthly = 0;
        [SerializeField]
        int _yearly = 0;

        public Boolean RepeatByDate = false;

        public DateTime DateStart
        {
            get
            {
                if (_tempDateStart != DateTime.MinValue)
                    return _tempDateStart;
                else
                {
                    _tempDateStart = DateTime.Parse(_dateStart);
                    return _tempDateStart;
                }
            }
            set
            {
                _dateStart = value.ToString();
                _tempDateStart = value;
            }
        }

        public DateTime DateEnd
        {
            get
            {
                if (_tempDateEnd != DateTime.MinValue)
                    return _tempDateEnd;
                else
                {
                    _tempDateEnd = DateTime.Parse(_dateEnd);
                    return _tempDateEnd;
                }
            }
            set
            {
                _dateEnd = (value >= DateStart) ? value.ToString() : DateStart.ToString();
                _tempDateEnd = (value >= DateStart) ? value : DateStart;
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }
        }

        public int Daily
        {
            get
            {
                return _daily;
            }
            set
            {
                if (value > 6) value = 6;

                if (Convert.ToInt32((DateEnd - DateStart).TotalDays) > 1) value = 0;
                _daily = value;
            }
        }

        public int Weekly
        {
            get
            {
                return _weekly;
            }
            set
            {
                if (value > 5) value = 5;

                _weekly = value;
            }
        }

        public int Monthly
        {
            get
            {
                return _monthly;
            }
            set
            {
                DateTime tempEndDate = DateEnd.AddMonths(value);

                if (tempEndDate.Year > DateEnd.Year)
                    value = value - tempEndDate.Month;

                _monthly = value;
            }
        }

        public int Yearly
        {
            get
            {
                return _yearly;
            }
            set
            {
                _yearly = value;
            }
        }
    }
}