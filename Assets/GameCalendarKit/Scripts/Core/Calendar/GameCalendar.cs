using GameCalendarKit.Clock;
using GameCalendarKit.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

namespace GameCalendarKit
{
    /// <summary>  
    ///  GameCalendar is the successor of MonoBehaviour and provides the necessary set of methods and events for creating game calendar and work with events collections.
    /// </summary> 
    [RequireComponent(typeof(GameClock))]
    public class GameCalendar : MonoBehaviour
    {
        public bool AutoInit = false;
        /// <summary>  
        ///  Pregenerated events ScriptableObject, you can find generator in menu GameTime -> Event Object Creator.  
        /// </summary> 
        [Header("Initial calendar events")]
        public EventsCollection BaseEventsCollection;

        /// <summary>  
        ///  Initial value. Here you can choose the year, month and date from which the time begins in the game.
        /// </summary> 
        [Header("Start calendar info")]
        [ShowOnly]
        public int StartYear;
        [ShowOnly]
        public int StartMonth;
        [ShowOnly]
        public int StartDay;
        [ShowOnly]
        public int DaysFromStart = 0;

        [Header("Start clock info")]
        [Range(0, 23)]
        public int ActiveHoursStart;
        [Range(0, 59)]
        public int ActiveMinutesStart;
        [Range(0, 23)]
        public int ActiveHoursEnd;
        [Range(0, 59)]
        public int ActiveMinutesEnd;

        [Header("Endless clock")]
        public bool Endless = false;

        /// <summary>  
        ///  This value provides the ability to set the number of seconds in one game second.
        ///  <para>
        ///    The minimum stable and guaranteed working value is 0.01f.
        ///  </para>
        /// </summary> 
        [Tooltip("How many real seconds in one game second?")]
        [Range(.00005f, 1f)]
        public float SecondsScale;


        ///  <remarks>
        ///    This value toggled when calendar starting and stopping to prevent double click.
        ///  </remarks>
        private bool _working = false;

        private bool _firstStart = true;

        private bool _ready = false;

        private DateTime _today;
        private DateTime _lastDay;
        private Calendar _calendar = CultureInfo.InvariantCulture.Calendar;
        private GameClock _clock;

        public List<GameCalendarEventObject> _events = new List<GameCalendarEventObject>();
        public List<GameCalendarEventObject> _eventsRepeated = new List<GameCalendarEventObject>();
        public List<GameCalendarEventObject> _eventsRepeatedByDate = new List<GameCalendarEventObject>();
        public List<GameCalendarEventObject> _eventsCompleted = new List<GameCalendarEventObject>();

        public List<GameCalendarEventObject> _eventsToday = new List<GameCalendarEventObject>();

        public bool Ready
        {
            get
            {
                return _ready;
            }

            private set
            {
                _ready = value;
            }
        }

        ///  <summary>
        ///    CalendarTimeChangedEvent is event method which will be launched every game seconds.
        ///  </summary>
        public event EventHandler<GameClockEventObject> CalendarTimeChangedEvent;

        ///  <summary>
        ///    CalendarDayEndEvent is event method which will be launched at the end of the working day.
        ///  </summary>
        public event EventHandler<GameClockEventObject> CalendarDayEndEvent;

        ///  <summary>
        ///    CalendarMidnightEvent is event method which will be launched at the 00:00 every day.
        ///  </summary>
        public event EventHandler<GameClockEventObject> CalendarMidnightEvent;

        ///  <summary>
        ///    CalendarEventStarted is event method which will be launched when any event start.
        ///  </summary>
        public event EventHandler<GameCalendarEventObjectList> CalendarEventStarted;

        ///  <summary>
        ///    CalendarEventEnded is event method which will be launched when any event ended.
        ///  </summary>
        public event EventHandler<GameCalendarEventObjectList> CalendarEventEnded;


        protected virtual void CalendarTimeChanged(GameClockEventObject args)
        {
            EventHandler<GameClockEventObject> delegateHandler = CalendarTimeChangedEvent;

            if (delegateHandler != null)
            {
                delegateHandler(this, args);
            }
        }

        protected virtual void CalendarDayEnd(GameClockEventObject args)
        {
            EventHandler<GameClockEventObject> delegateHandler = CalendarDayEndEvent;

            if (delegateHandler != null)
            {
                delegateHandler(this, args);
            }
        }

        protected virtual void CalendarMidnight(GameClockEventObject args)
        {
            EventHandler<GameClockEventObject> delegateHandler = CalendarMidnightEvent;

            if (delegateHandler != null)
            {
                delegateHandler(this, args);
            }
        }

        protected virtual void CalendarEventStart(GameCalendarEventObjectList args)
        {
            EventHandler<GameCalendarEventObjectList> delegateHandler = CalendarEventStarted;

            if (delegateHandler != null)
            {
                delegateHandler(this, args);
            }
        }

        protected virtual void CalendarEventEnd(GameCalendarEventObjectList args)
        {
            EventHandler<GameCalendarEventObjectList> delegateHandler = CalendarEventEnded;

            if (delegateHandler != null)
            {
                delegateHandler(this, args);
            }
        }

        public void Awake()
        {
            _clock = GetComponent<GameClock>();

            if (AutoInit)
                InitGameCalendar();
        }

        /// <summary>  
        ///  InitGameCalendar is a method for initializing the calendar.
        /// </summary> 
        private void InitGameCalendar()
        {
            if (Ready) return;

            _today = new DateTime(StartYear, StartMonth, StartDay, ActiveHoursStart, ActiveMinutesStart, 0, new GregorianCalendar());

            InitGameClock(ActiveHoursStart, ActiveMinutesStart, ActiveHoursEnd, ActiveMinutesEnd, SecondsScale);

            if (BaseEventsCollection != null)
            {
                foreach (var baseEvent in BaseEventsCollection.Events)
                {
                    CreateEvent(baseEvent);
                }
            }
        }

        /// <summary>  
        ///  Method for manual initializing the calendar by DateTime variable.
        /// </summary> 
        public void InitGameCalendar(DateTime initDate)
        {
            if (Ready) return;

            _today = initDate;

            if (BaseEventsCollection != null)
            {
                foreach (var baseEvent in BaseEventsCollection.Events)
                {
                    CreateEvent(baseEvent);
                }
            }
        }

        /// <summary>  
        ///  Method for manual initializing the calendar by timestamp variable.
        /// </summary> 
        public void InitGameCalendar(int timestamp)
        {
            if (Ready) return;

            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, new GregorianCalendar());
            dtDateTime.AddSeconds(timestamp);

            _today = dtDateTime;

            if (BaseEventsCollection != null)
            {
                foreach (var baseEvent in BaseEventsCollection.Events)
                {
                    CreateEvent(baseEvent);
                }
            }
        }

        /// <summary>  
        ///  Method for manual initializing the calendar by separately variable.
        /// </summary> 
        public void InitGameCalendar(int startYear, int startMonth, int startDay, int hoursStart, int minutesStart)
        {
            if (Ready) return;

            _today = new DateTime(startYear, startMonth, startDay, hoursStart, 0, 0, new GregorianCalendar());

            if (BaseEventsCollection != null)
            {
                foreach (var baseEvent in BaseEventsCollection.Events)
                {
                    CreateEvent(baseEvent);
                }
            }
        }

        /// <summary>  
        ///  Method for manual initializing GameClock by separately variable.
        /// </summary> 
        public void InitGameClock(int startHour, int startMinute, int endHour, int endMinute, float scale)
        {
            if (Ready) return;

            if (!_clock)
                _clock = GetComponent<GameClock>();

            _clock.GameClockInit(startHour, startMinute, endHour, endMinute, scale, Endless);
            _clock.DayEndEvent += DayEnd;
            _clock.MidnightEvent += Midnight;
            _clock.TimeChangedEvent += TimeChanged;

            Ready = true;
        }


        /// <summary>  
        ///  StartNewDay is a method for starting new day.
        ///  <para>
        ///    First day also start here.
        ///  </para>
        /// </summary> 
        public void StartNewDay()
        {
            if (Ready)
            {
                _working = true;

                RemoveEndedEvents(_today);

                _today = new DateTime(_today.Year, _today.Month, _today.Day, ActiveHoursStart, ActiveMinutesStart, 0, new GregorianCalendar());

                if (!_firstStart)
                {
                    if (_today.Day == _lastDay.Day && _lastDay.Hour >= ActiveHoursStart && _lastDay.Minute >= ActiveMinutesStart)
                        _today = _calendar.AddDays(_today, 1);
                }

                _lastDay = _today;

                _clock.StartClock();

                CheckEvent();

                if (_firstStart)
                {
                    FillDaylyEventsList(_today);
                    _firstStart = false;
                }
            }
            else
                throw new Exception("Calendar is not initialized");
        }

        /// <summary>  
        ///  RestartDay is a method for restart current day.
        /// </summary> 
        public void RestartDay()
        {
            if (Ready)
            {
                if (_today.Hour == ActiveHoursEnd && _today.Minute == ActiveMinutesEnd && DaysFromStart > 0)
                    DaysFromStart--;

                _working = true;

                _today = new DateTime(_lastDay.Year, _lastDay.Month, _lastDay.Day, ActiveHoursStart, ActiveMinutesStart, 0, new GregorianCalendar());
                _clock.StartClock();

                CheckEvent();
            }
            else
                throw new Exception("Calendar is not initialized");
        }

        /// <summary>  
        ///  PauseDay is a method for pause calendar time.
        ///  <para>
        ///    Stops only calendar, not the all game.
        ///  </para>
        /// </summary> 
        public void PauseDay()
        {
            if (!_working) return;

            if (Ready)
            {
                _working = false;
                _clock.PauseClock();
            }
            else
                throw new Exception("Calendar is not initialized");
        }

        /// <summary>  
        ///  ContinueDay is a method for continue calendar time if it was paused.
        ///  <para>
        ///    Continue tick from paused moment.
        ///  </para>
        /// </summary> 
        public void ContinueDay()
        {
            if (_working) return;

            if (Ready)
            {
                _working = true;
                _clock.ContinueClock();
            }
            else
                throw new Exception("Calendar is not initialized");
        }

        /// <summary>  
        ///  GetCurrentDate return current game date.
        /// </summary> 
        public DateTime GetCurrentDate()
        {
            if (Ready)
                return _today;
            else
                throw new Exception("Calendar is not initialized");
        }

        /// <summary>  
        ///  GetCurrentDateTimestamp return current game date in unix timestamp.
        /// </summary> 
        public int GetCurrentDateTimestamp()
        {
            if (Ready)
                return (int)(_today.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            else
                throw new Exception("Calendar is not initialized");
        }

        /// <summary>  
        ///  GetCurrentDateArray return current game date in unix timestamp.
        /// </summary> 
        public int[] GetCurrentDateArray()
        {
            if (Ready)
                return new int[] { _today.Year, _today.Month, _today.Day, _today.Hour, _today.Minute };
            else
                throw new Exception("Calendar is not initialized");
        }

        private void FillDaylyEventsList(DateTime day)
        {
            _eventsToday.Clear();
            _eventsToday = GetAllEventsByDate(day);
        }

        /// <summary>  
        ///  CreateEvent put new GameCalendarEventObject object to calendar events collection.
        /// </summary> 
        public void CreateEvent(GameCalendarEventObject newEvent)
        {
            if (newEvent.Daily == 0 && newEvent.Weekly == 0 && newEvent.Monthly == 0 && newEvent.Yearly == 0)
                _events.Add(newEvent);
            else if (newEvent.RepeatByDate)
                _eventsRepeatedByDate.Add(newEvent);
            else
                _eventsRepeated.Add(newEvent);

            FillDaylyEventsList(_today);
            SortEvents();
        }

        private void SortEvents()
        {
            _events.Sort((x, y) => DateTime.Compare(x.DateStart, y.DateStart));
            _eventsRepeated.Sort((x, y) => DateTime.Compare(x.DateStart, y.DateStart));
            _eventsRepeatedByDate.Sort((x, y) => DateTime.Compare(x.DateStart, y.DateStart));
            _eventsCompleted.Sort((x, y) => DateTime.Compare(x.DateStart, y.DateStart));
            _eventsToday.Sort((x, y) => DateTime.Compare(x.DateStart, y.DateStart));
        }

        /// <summary>  
        ///  CheckEvent private method to find any event for current day from all list except _eventsCompleted list.
        /// </summary> 
        private void CheckEvent()
        {
            List<GameCalendarEventObject> start = _events.FindAll(e => e.DateStart.CompareTo(_today) == 0);
            List<GameCalendarEventObject> end = _events.FindAll(e => e.DateEnd.CompareTo(_today) == 0);

            for (var i = 0; i < _eventsToday.Count; i++)
            {
                if (TimeComparer(_eventsToday[i].DateStart, _today))
                    start.Add(_eventsToday[i]);
                else if (TimeComparer(_eventsToday[i].DateEnd, _today))
                    end.Add(_eventsToday[i]);
            }

            GameCalendarEventObjectList eventStartObj = new GameCalendarEventObjectList(start);
            GameCalendarEventObjectList eventEndObj = new GameCalendarEventObjectList(end);

            if (eventStartObj.Events.Count > 0)
                CalendarEventStart(eventStartObj);

            if (eventEndObj.Events.Count > 0)
                CalendarEventEnd(eventEndObj);
        }

        /// <summary>  
        ///  CheckEventRepeatByDate private method to find any event for requested day, repeated by current date(every 1 september).
        /// </summary> 
        private GameCalendarEventObject[] CheckEventRepeatByDate(GameCalendarEventObject eventObject, DateTime checkDate)
        {

            GameCalendarEventObject[] array = new GameCalendarEventObject[] { null, null };

            var startDate = eventObject.DateStart.AddMonths(eventObject.Monthly);
            startDate = startDate.AddYears(eventObject.Yearly);

            var endDate = eventObject.DateEnd.AddMonths(eventObject.Monthly);
            endDate = endDate.AddYears(eventObject.Yearly);

            if (
                YearComparer(eventObject.DateStart, startDate, checkDate)
                &&
                MonthComparer(eventObject.DateStart, startDate, checkDate)
                &&
                eventObject.DateStart.Day == checkDate.Day
                )
                array[0] = eventObject;

            if (
                YearComparer(eventObject.DateEnd, endDate, checkDate)
                &&
                MonthComparer(eventObject.DateEnd, endDate, checkDate)
                &&
                eventObject.DateEnd.Day == checkDate.Day
                )
                array[1] = eventObject;

            return array;
        }

        /// <summary>  
        ///  CheckEventByDate private method to find any event for requested date.
        /// </summary> 
        private GameCalendarEventObject[] CheckEventByDate(GameCalendarEventObject eventObject, DateTime checkDate)
        {
            GameCalendarEventObject[] array = new GameCalendarEventObject[] { null, null };

            DateTime dateStart = eventObject.DateStart;
            DateTime dateEnd = eventObject.DateEnd;

            DateTime YS = dateStart.AddMonths(eventObject.Yearly * 12);
            DateTime YE = dateEnd.AddMonths(eventObject.Yearly * 12);

            if
            (
                YearComparer(dateStart, YS, checkDate)
                &&
                MonthComparer(dateStart, dateStart.AddMonths(eventObject.Monthly), checkDate)
                &&
                WeekComparer(dateStart, dateStart.AddDays(eventObject.Daily), checkDate, eventObject.Weekly)
                &&
                DayComparer(dateStart, checkDate, eventObject)
            )
            {
                array[0] = eventObject;
            }
            else if
            (
                YearComparer(dateEnd, YE, checkDate)
                &&
                MonthComparer(dateEnd, dateEnd.AddMonths(eventObject.Monthly), checkDate)
                &&
                WeekComparer(dateEnd, dateEnd.AddDays(eventObject.Daily), checkDate, eventObject.Weekly)
                &&
                DayComparer(dateEnd, checkDate, eventObject)
            )
            {
                array[1] = eventObject;
            }

            return array;
        }

        /// <summary>  
        ///  TimeComparer private method to check time, used by CheckEvent on every tick.
        /// </summary> 
        private bool TimeComparer(DateTime left, DateTime right)
        {
            return left.Hour == right.Hour && left.Minute == right.Minute && right.Second == left.Second;
        }

        /// <summary>  
        ///  DayComparer private method to check day, used by CheckEventByDate on request eg: DayComparer(dateEnd, checkDate, eventObject).
        /// </summary> 
        private bool DayComparer(DateTime start, DateTime origin, GameCalendarEventObject eventObject)
        {
            DateTime tempTime = new DateTime(start.Year, start.Month, start.Day);
            if (DateTime.Compare(tempTime, origin) > 0) return false;

            if (eventObject.Daily > 0)
                for (int i = 0; i <= eventObject.Daily; i++)
                {
                    DateTime temp = start;
                    temp = temp.AddDays(i);
                    if (temp.DayOfWeek == origin.DayOfWeek)
                        return true;
                }
            else
                return start.DayOfWeek == origin.DayOfWeek;

            return false;
        }

        /// <summary>  
        ///  GetWeekOfMonth private method return number of week in requested month.
        /// </summary> 
        private int GetWeekOfMonth(DateTime time)
        {
            DateTime first = new DateTime(time.Year, time.Month, 1);
            return GetWeekOfYear(time) - GetWeekOfYear(first)+1;
        }

        /// <summary>  
        ///  GetWeekOfYear private method return number of week in requested year.
        /// </summary> 
        private int GetWeekOfYear(DateTime fromDate)
        {
            return _calendar.GetWeekOfYear(fromDate, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
        }

        /// <summary>  
        ///  WeekComparer private method return true if start week smaller than current and week + modificator bigger than current.
        /// </summary> 
        private bool WeekComparer(DateTime baseTime, DateTime endTime, DateTime origin, int modificator)
        {
            if (baseTime.DayOfWeek > endTime.DayOfWeek)
                modificator++;
            return GetWeekOfMonth(baseTime) <= GetWeekOfMonth(origin) && GetWeekOfMonth(baseTime) + modificator >= GetWeekOfMonth(origin);
        }

        /// <summary>  
        ///  MonthComparer private method return true if start month smaller than origin and right bigger than origin.
        /// </summary> 
        private bool MonthComparer(DateTime start, DateTime end, DateTime origin)
        {
            return start.Month <= origin.Month && end.Month >= origin.Month;
        }

        /// <summary>  
        ///  YearComparer same as month, but for year compare.
        /// </summary> 
        private bool YearComparer(DateTime left, DateTime right, DateTime origin)
        {
            return left.Year <= origin.Year && right.Year >= origin.Year;
        }

        /// <summary>  
        ///  GetAllEventsByDate return all GameCalendarEventObjectList that matches requested date.
        /// </summary> 
        public List<GameCalendarEventObject> GetAllEventsByDate(DateTime date)
        {
            List<GameCalendarEventObject> events = _events.Where(e =>
            (e.DateStart.Year == date.Year && e.DateStart.Month == date.Month && e.DateStart.Day == date.Day)
            ||
            (e.DateEnd.Year == date.Year && e.DateEnd.Month == date.Month && e.DateEnd.Day == date.Day)
            ).ToList();


            for (var i = 0; i < _eventsRepeated.Count; i++)
            {
                var temp = CheckEventByDate(_eventsRepeated[i], date);

                if (temp[0] != null)
                    events.Add(temp[0]);
                if (temp[1] != null)
                    events.Add(temp[1]);
            }

            for (var i = 0; i < _eventsRepeatedByDate.Count; i++)
            {
                var temp = CheckEventRepeatByDate(_eventsRepeatedByDate[i], date);

                if (temp[0] != null)
                    events.Add(temp[0]);
                if (temp[1] != null)
                    events.Add(temp[1]);
            }

            return events;
        }

        /// <summary>  
        ///  RemoveEndedEvents private method for copy event that was ended into _eventsCompleted list and remove from origin list.
        /// </summary> 
        private void RemoveEndedEvents(DateTime date)
        {
            for (var i = 0; i < _eventsRepeated.Count; i++)
            {
                var e = ReturnEndedEvents(_eventsRepeated[i], date);

                if(e != null)
                    _eventsCompleted.Add(e);
            }
            for (var i = 0; i < _eventsRepeatedByDate.Count; i++)
            {
                var e = ReturnEndedEvents(_eventsRepeatedByDate[i], date);

                if (e != null)
                    _eventsCompleted.Add(e);
            }
            for (var j = 0; j < _events.Count; j++)
            {
                _eventsCompleted.Add(ReturnEndedEvents(_events[j], date));
            }
            for (var c = 0; c < _eventsCompleted.Count; c++)
            {
                _eventsRepeated.Remove(_eventsCompleted[c]);
                _eventsRepeatedByDate.Remove(_eventsCompleted[c]);
                _events.Remove(_eventsCompleted[c]);
            }
        }

        /// <summary>  
        ///  ReturnEndedEvents private method helper.
        /// </summary> 
        private GameCalendarEventObject ReturnEndedEvents(GameCalendarEventObject eventObject, DateTime checkDate)
        {
            
            DateTime dateEnd = eventObject.DateEnd;

            DateTime dateEndY = dateEnd.AddYears(eventObject.Yearly);
            DateTime dateEndM = dateEndY.AddMonths(eventObject.Monthly);
            DateTime dateEndW = dateEndM.AddDays(eventObject.Weekly * 7);
            DateTime dateEndD = dateEndW.AddDays(eventObject.Daily);

            if
            (
                DateTime.Compare(dateEndD, checkDate) >= 0
            )
                return null;
            else
                return eventObject;
        }

        /// <summary>  
        ///  GetAllEvents return all event in GameCalendarEventObjectList.
        /// </summary> 
        public List<GameCalendarEventObject> GetAllEvents()
        {
            List<GameCalendarEventObject> events = new List<GameCalendarEventObject>();
            events.AddRange(_events);
            events.AddRange(_eventsRepeated);
            events.AddRange(_eventsCompleted);
            return events;
        }

        /// <summary>  
        ///  ChangeTimeScale change global timeScale to transmitted value.
        /// </summary> 
        public void ChangeTimeScale(float timeScale)
        {
            if(Ready)
                Time.timeScale = timeScale;
            else
                throw new Exception("Calendar is not initialized");
        }

        /// <summary>  
        ///  ChangeActiveHours change active hours to transmitted value.
        ///  <para>
        ///    Will affected on next StartNewDay request.
        ///  </para>
        /// </summary> 
        public void ChangeActiveHours(int startTime, int endTime)
        {
            if (Ready)
            {
                ActiveHoursStart = startTime;
                ActiveHoursEnd = endTime;

                _clock.ChangeActiveHours(ActiveHoursStart, ActiveHoursEnd);
            }
            else
                throw new Exception("Calendar is not initialized");
        }

        /// <summary>  
        ///  DayEnd fires when clock event DayEndEvent is started. Method start day end delegate.
        /// </summary> 
        private void DayEnd(object source, GameClockEventObject clockObject)
        {
            CalendarDayEnd(clockObject);

            if (!Endless)
            {
                DaysFromStart++;
                FillDaylyEventsList(_today.AddDays(1));
            }
        }

        /// <summary>
        ///  Midnight fires when clock fire event MidnightEvent.
        /// </summary>
        private void Midnight(object source, GameClockEventObject clockObject)
        {
            CalendarMidnight(clockObject);

            if (Endless)
            {
                DaysFromStart++;
                FillDaylyEventsList(_today.AddDays(1));
            }
        }

        /// <summary>  
        ///  MinuteEnd fires when clock event MinuteEndEvent is started. Method add new minute to current calendar and start checking events.
        /// </summary> 
        private void TimeChanged(object source, GameClockEventObject clockObject)
        {
            _today = _calendar.AddSeconds(_today, 1);
            CalendarTimeChanged(clockObject);
            CheckEvent();
        }
    }
}

