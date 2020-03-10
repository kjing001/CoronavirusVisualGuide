using System;
using UnityEditor;
using UnityEngine;


namespace GameCalendarKit.Helpers
{
    [CustomEditor(typeof(GameCalendar))]
    [CanEditMultipleObjects]
    public class CalendarEditor : Editor
    {
        protected GameCalendar calendar;

        public int Year = DateTime.Now.Year;
        public Months Month = Months.January;
        public int Day = 0;
        public int Hour = 0;
        public int Minute = 0;
        public string[] Days;

        public DateTime _startDate = DateTime.MinValue;

        public enum Months
        {
            January = 1,
            February = 2,
            March = 3,
            April = 4,
            May = 5,
            June = 6,
            July = 7,
            August = 8,
            September = 9,
            October = 10,
            November = 11,
            December = 12
        }

        void OnEnable()
        {
            calendar = (GameCalendar)target;
            CalculateDays();
            calendar.StartYear = Year;
            calendar.StartDay = int.Parse(Days[Day]);
            calendar.StartMonth = (int)Month;
        }
        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField("Select start date");

            GUILayout.BeginVertical("Box");

            EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(300));

            if (GUILayout.Button("-", GUILayout.Width(30)))
            {
                Year--;
                calendar.StartYear = Year;
            }
            EditorGUILayout.LabelField("Year: " + Year, GUILayout.MaxWidth(80));
            if (GUILayout.Button("+", GUILayout.Width(30)))
            {
                Year++;
                calendar.StartYear = Year;
            }


            Month = (Months)EditorGUILayout.EnumPopup(Month, GUILayout.MinWidth(40));

            calendar.StartMonth = (int)Month;

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Separator();

            Day = GUILayout.SelectionGrid(Day, Days, 7);


            calendar.StartDay = int.Parse(Days[Day]);

            GUILayout.EndVertical();

            CalculateDays();
            base.OnInspectorGUI();

            EditorUtility.SetDirty(calendar);
        }

        private void CalculateDays()
        {
            if (Day + 1 > DateTime.DaysInMonth(Year, (int)Month))
                Day = 0;

            DateTime iDay = DateTime.Now;
            Days = new string[DateTime.DaysInMonth(Year, (int)Month)];

            for (int i = 0; i < DateTime.DaysInMonth(Year, (int)Month); i++)
            {
                iDay = new DateTime(Year, (int)Month, i + 1);
                Days[i] = iDay.ToString("dd");
            }
        }
    }
}