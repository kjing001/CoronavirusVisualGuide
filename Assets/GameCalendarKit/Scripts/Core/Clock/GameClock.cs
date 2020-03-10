using GameCalendarKit.TickerUtil;
using System;
using UnityEngine;

namespace GameCalendarKit.Clock
{
    /// <summary>  
    ///  GameClock is the successor of MonoBehaviour and provides the necessary set of methods and events for creating game time.
    ///  <para>
    ///    Can be used wthout GameCalendar to check time from start to end values.
    ///  </para>
    /// </summary> 
    [RequireComponent(typeof(Ticker))]
    public class GameClock : MonoBehaviour
    {
        private bool _endless;
        private bool _inited;

        private int _ticks = 60;
        private int _initStartHours;
        private int _initStartMinutes;
        private int _initEndHours;
        private int _initEndMinutes;
        private int _endHours;
        private int _currentHour;
        private int _minutes;
        private int _endMinutes;
        private int _seconds;

        private Ticker _ticker;

        ///  <summary>
        ///    TimeChangedEvent is a event method which will be launched when game time changed.
        ///  </summary>
        public event EventHandler<GameClockEventObject> TimeChangedEvent;

        ///  <summary>
        ///    DayEndEvent is a event method which will be launched when game day ending.
        ///  </summary>
        public event EventHandler<GameClockEventObject> DayEndEvent;

        ///  <summary>
        ///    DayEndEvent is a event method which will be launched when game day ending.
        ///  </summary>
        public event EventHandler<GameClockEventObject> MidnightEvent;


        protected virtual void TimeChanged(GameClockEventObject args)
        {
            EventHandler<GameClockEventObject> delegateHandler = TimeChangedEvent;

            if (delegateHandler != null)
            {
                delegateHandler(this, args);
            }
        }

        protected virtual void DayEnd(GameClockEventObject args)
        {
            EventHandler<GameClockEventObject> delegateHandler = DayEndEvent;

            if (delegateHandler != null)
            {
                delegateHandler(this, args);
            }
        }

        protected virtual void Midnight(GameClockEventObject args)
        {
            EventHandler<GameClockEventObject> delegateHandler = MidnightEvent;

            if (delegateHandler != null)
            {
                delegateHandler(this, args);
            }
        }

        private void Awake()
        {
            _ticker = GetComponent<Ticker>();
        }

        /// <summary>  
        ///  GameClockInit is an initialization method which set a start values to ticker and add to event TickEvent, method ClockWork.
        ///  <para>
        ///    Request three arguments: startTime - from this value it will start tick; endTime - when clock reach this value DayEndEvent will fired up; interval - this value tell how many seconds will be in one game minute.
        ///  </para>
        /// </summary> 
        public void GameClockInit(int startHour, int startMinute, int endHour, int endMinute, float interval, bool endless)
        {
            if (_inited) return;

            _initStartHours = startHour;
            _initStartMinutes = startMinute;

            _initEndHours = endHour;
            _initEndMinutes = endMinute;


            _endless = endless;

            if(!_ticker)
                _ticker = GetComponent<Ticker>();

            _ticker.TickerInit(_ticks, interval);
            _ticker.TickEvent += ClockWork;

            _inited = true;
        }

        ///  <summary>
        ///    StartClock is a method that start clock work.
        ///  </summary>
        public void StartClock()
        {
            if (!_inited) return;

            _currentHour = _initStartHours;
            _endHours = _initEndHours;
            _minutes = _initStartMinutes;
            _endMinutes = _initEndMinutes;

            _ticker.StartNewTimer();
        }

        ///  <summary>
        ///    PauseClock is a method for pause clock time.
        ///  <para>
        ///   Stop only this clock time, Unity time is still work.
        ///  </para>
        ///  </summary>
        public void PauseClock()
        {
            if (!_inited) return;

            _ticker.PauseTimer();
        }

        ///  <summary>
        ///    ContinueClock is a method for continue clock time.
        ///  <para>
        ///   Continue from paused value.
        ///  </para> 
        ///  </summary>
        public void ContinueClock()
        {
            if (!_inited) return;

            _ticker.StartTimer();
        }

        /// <summary>  
        ///  ChangeActiveHours change _initStartHours and _initEndHours to transmitted value.
        ///  <para>
        ///    Will affected on next StartClock request.
        ///  </para>
        /// </summary> 
        public void ChangeActiveHours(int startTime, int endTime)
        {
            _initStartHours = startTime;
            _initEndHours = endTime;
        }

        //Clock calculation
        private void ClockWork(object source, TickEventObject e)
        {
            _seconds = e.CurrentTick;

            if (!e.Status)
            {
                _minutes++;

                if (_minutes == 60)
                {
                    _minutes = 0;
                    _currentHour++;

                    if (_currentHour == 24)
                        _currentHour = 0;
                }

                if (_currentHour == 00 && _minutes == 00)
                    Midnight(new GameClockEventObject(false, _currentHour, _minutes, _seconds));

                if (_currentHour == _endHours && _minutes == _endMinutes && !_endless)
                {
                    DayEnd(new GameClockEventObject(false, _currentHour, _minutes, _seconds));
                }
                else
                    _ticker.StartNewTimer();
            }

            TimeChanged(new GameClockEventObject(true, _currentHour, _minutes, _seconds));
        }
    }
}