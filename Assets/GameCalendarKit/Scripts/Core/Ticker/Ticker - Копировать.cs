//using System;
//using System.Collections;
//using UnityEngine;

//namespace GameCalendarKit.TickerUtil
//{
//    ///  <summary>
//    ///    Ticker is the successor of MonoBehaviour and provides the necessary set of methods and events for make timer counting from A to B value.
//    ///  <para>
//    ///    Can be used wthout GameCalendar and GameClock.
//    ///  </para>
//    ///  </summary>
//    public class Ticker : MonoBehaviour
//    {
//        /// <summary>  
//        ///  TickerInit is a initiated method which set a start values to ticker.
//        ///  <para>
//        ///    Request two arguments: ticks - How many ticks will be counted; interval - The time interval between tick.
//        ///  </para>
//        /// </summary> 
//        public void TickerInit(int ticks, float interval)
//        {
//            _maxTicks = ticks;
//            _currentTick = 0;
//            _interval = interval;
//        }

//        private int _maxTicks;
//        private int _currentTick;

//        private float _interval;
//        private float currentTime;

//        private float Timer = 1;

//        private bool on = false;

//        /// <summary>  
//        ///  TickEvent is a event method which will be launched when tick counted.
//        /// </summary> 
//        public event EventHandler<TickEventObject> TickEvent;

//        protected virtual void Tick(TickEventObject args)
//        {
//            EventHandler<TickEventObject> delegateHandler = TickEvent;

//            if (delegateHandler != null)
//            {
//                delegateHandler(this, args);
//            }
//        }

//        /// <summary>  
//        ///  StartNewTimer is a method to start new timer with initial values.
//        ///  <para>
//        ///    Before start new, it will stop previous one if it already launched ad drop _currentTick counter to zero.
//        ///  </para> 
//        /// </summary> 
//        public void StartNewTimer()
//        {
//            on = true;
//            Timer = 1 * _interval;
//            //CancelInvoke("OnTimedEvent");
//            //_currentTick = 0;
//            //InvokeRepeating("OnTimedEvent", _interval, _interval);
//        }

//        /// <summary>  
//        ///  StartTimer is a method to continue timer from paused value.
//        ///  <para>
//        ///    If current _currentTick == _maxTicks it will brake execution and fireup Tick event with false and 0 arguments.
//        ///  </para> 
//        /// </summary> 
//        public void StartTimer()
//        {
//            //InvokeRepeating("OnTimedEvent", _interval, _interval);

//            on = true;
//        }

//        void FixedUpdate()
//        {

//            if (on == true)
//            {
//                Timer -= Time.deltaTime;

//                if (Timer <= 0f)
//                {
//                    OnTimedEvent();
//                    Timer = 1 * _interval;
//                    //You can send the player back to the start here if you want

//                }
//            }

//        }
//        /// <summary>  
//        ///  StopTimer is a method to stop timer.
//        ///  <para>
//        ///   Also will fireup Tick event whit curent tick.
//        ///  </para> 
//        /// </summary> 
//        public void StopTimer()
//        {
//            on = false;
//            //CancelInvoke("OnTimedEvent");
//            Tick(new TickEventObject(false, _currentTick));
//        }

//        /// <summary>  
//        ///  PauseTimer is a method to stop timer without any message.
//        /// </summary>
//        public void PauseTimer()
//        {
//            on = false;
//            CancelInvoke("OnTimedEvent");
//        }

//        //Ticker calculate
//        private void OnTimedEvent()
//        {

//            _currentTick++;

//            if (_currentTick == _maxTicks)
//            {
//                StopTimer();
//                return;
//            }

//            Tick(new TickEventObject(true, _currentTick));
//        }


//    }
//}