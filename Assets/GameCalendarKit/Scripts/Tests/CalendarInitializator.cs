//using UnityEngine;
//using UnityEngine.TestTools;
//using NUnit.Framework;
//using System.Collections;
//using System;
//using System.Globalization;

//namespace GameCalendarKit.Tests
//{
//    public class CalendarInitializator
//    {
//        [Test]
//        public void InitGameCalendarTimeStamp()
//        {
//            // Use the Assert class to test conditions.
//            var go = new GameObject();
//            go.AddComponent<GameCalendar>();

//            GameCalendar calendar = go.GetComponent<GameCalendar>();
//            calendar.InitGameCalendar(175);
//            calendar.InitGameClock(1, 2, 2, 2, 1);

//            var date = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
//            date.AddSeconds(175);

//            Assert.AreEqual(calendar.GetCurrentDate(), date);
//        }

//        [Test]
//        public void InitGameCalendarDateTime()
//        {
//            // Use the Assert class to test conditions.
//            var go = new GameObject();
//            go.AddComponent<GameCalendar>();

//            var date = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

//            GameCalendar calendar = go.GetComponent<GameCalendar>();
//            calendar.InitGameCalendar(date);
//            calendar.InitGameClock(1, 2, 2, 2, 1);

//            Assert.AreEqual(calendar.GetCurrentDate(), date);
//        }

//        [Test]
//        public void InitGameCalendarSeparateValues()
//        {
//            // Use the Assert class to test conditions.
//            var go = new GameObject();
//            go.AddComponent<GameCalendar>();

//            var date = new DateTime(1970, 1, 1, 0, 0, 0, new GregorianCalendar());

//            GameCalendar calendar = go.GetComponent<GameCalendar>();
//            calendar.InitGameCalendar(1970, 1, 1, 0, 0);
//            calendar.InitGameClock(1, 2, 2, 2, 1);

//            Assert.AreEqual(calendar.GetCurrentDate(), date);
//        }
//    }
//}
