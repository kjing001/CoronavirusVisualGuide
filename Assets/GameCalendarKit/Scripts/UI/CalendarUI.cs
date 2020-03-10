using GameCalendarKit.Clock;
using System;
using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace GameCalendarKit.Example
{

    public class CalendarUI : MonoBehaviour
    {

        [Header("Dropdowns")]
        public Dropdown MonthSelector;
        public Dropdown HourSelector;
        public Dropdown MinuteSelector;

        public Dropdown ActiveStartHourDropdown;
        public Dropdown ActiveEndHourDropdown;

        [Header("Text inputs")]
        public InputField TitleInput;
        public InputField TextInput;

        [Header("UI object reference")]
        public RectTransform CanvasPlane;

        public Transform CalendarBody;
        public Transform CalendarBodySunday;
        public Transform CalendarBodyMonday;
        public Transform CalendarBodyTuesday;
        public Transform CalendarBodyWednesday;
        public Transform CalendarBodyThursday;
        public Transform CalendarBodyFriday;
        public Transform CalendarBodySaturday;

        public Transform CalendarEventsContainer;

        public Button DayPrefab;
        public Button StartButton;
        public Button SaveStartTimeButton;
        public Button CreateEventButton;

        public Button ChangeActiveHoursButton;

        public Text CalendarYearOutput;
        public Text CurentDate;
        public Text MidnightNotification;
        public Text DaysFromStart;
        public Text CurentTime;
        public Text EventStatus;
        public Text EventTitle;
        public Text EventText;
        public Text InfoText;
        public Text EventPrefab;

        public InputField DailyRepeat;
        public InputField WeeklyRepeat;
        public InputField MonthlyRepeat;
        public InputField YearlyRepeat;

        public Toggle RepeatByDate;

        private enum Months
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

        private int Year;
        private Months Month;
        private int Day = 1;
        private int Hour = 0;
        private int Minute = 0;

        //This ints used in loop, to fill Dropdowns
        private int _hours = 23;
        private int _minutes = 59;

        private DateTime _startTime;
        private DateTime _endTime;
        public GameCalendar Calendar;

        void Awake()
        {
            Calendar.CalendarEventStarted += GetEventStart;
            Calendar.CalendarEventEnded += GetEventEnd;
            Calendar.CalendarDayEndEvent += DayOver;
            Calendar.CalendarMidnightEvent += Midnight;
            Calendar.CalendarTimeChangedEvent += TimeChanged;

            StartButton.onClick.AddListener(StartDay);

            Year = Calendar.GetCurrentDate().Year;
            CalendarYearOutput.text = Year.ToString();
            Month = (Months)Calendar.GetCurrentDate().Month;

            MonthSelector.options.Clear();
            HourSelector.options.Clear();
            MinuteSelector.options.Clear();
            ActiveStartHourDropdown.options.Clear();
            ActiveEndHourDropdown.options.Clear();


            foreach (Months foo in Enum.GetValues(typeof(Months)))
            {
                Dropdown.OptionData newOptions = new Dropdown.OptionData(foo.ToString());
                MonthSelector.options.Add(newOptions);
            }

            MonthSelector.value = (int)Month - 1 ;
            HourMinuteIterator(MinuteSelector, _minutes);
            HourMinuteIterator(HourSelector, _hours);
            HourMinuteIterator(ActiveStartHourDropdown, _hours);
            HourMinuteIterator(ActiveEndHourDropdown, _hours);

            PrintCalendarGUI();
            PrintTime();
        }

        private void HourMinuteIterator(Dropdown dropObject, int count)
        {
            for (int i = 0; i <= count; i++)
            {
                Dropdown.OptionData newOptions = new Dropdown.OptionData((i.ToString().Length < 2) ? "0" + i.ToString() : i.ToString());
                dropObject.options.Add(newOptions);
            }
        }

        public void SetStartTime()
        {
            InfoText.text = "Select end time";
            _startTime = new DateTime(int.Parse(CalendarYearOutput.text), (int)Month, Day, Hour, Minute, 0);

        }

        public void SetEndTime()
        {
            InfoText.text = "Select repeat";
            _endTime = new DateTime(int.Parse(CalendarYearOutput.text), (int)Month, Day, Hour, Minute, 0);

            RepeaterCheckBoxChecker();
        }

        public void CreateEvent()
        {
            InfoText.text = "Select start time";

            Calendar.CreateEvent(
                new GameCalendarEventObject(
                    _startTime,
                    _endTime,
                    TitleInput.text,
                    TextInput.text,
                    Convert.ToInt32(DailyRepeat.text),
                    Convert.ToInt32(WeeklyRepeat.text),
                    Convert.ToInt32(MonthlyRepeat.text),
                    Convert.ToInt32(YearlyRepeat.text),
                    RepeatByDate.isOn
                ));

            TitleInput.text = "";
            TextInput.text = "";

            PrintCalendarGUI();
        }

        private void GetEventStart(object source, GameCalendarEventObjectList eventObject)
        {
            if (eventObject == null) return;

            EventTitle.text = string.Empty;
            EventText.text = string.Empty;

            EventStatus.text = "Event start";

            foreach (GameCalendarEventObject eve in eventObject.Events)
            {
                GameCalendarEventObject eventObj = eve;
                EventTitle.text += eventObj.Title;
                EventText.text += eventObj.Text;
            }

            StartCoroutine(ClearTextField(EventTitle, 2f));
            StartCoroutine(ClearTextField(EventText, 2f));
            StartCoroutine(ClearTextField(EventStatus, 2f));

            Debug.Log("Event started in " + CurentDate.text + " " + CurentTime.text);
        }

        private void GetEventEnd(object source, GameCalendarEventObjectList clockObject)
        {
            if (clockObject == null) return;

            EventTitle.text = string.Empty;
            EventText.text = string.Empty;

            EventStatus.text = "Event end";

            foreach (GameCalendarEventObject eve in clockObject.Events)
            {
                GameCalendarEventObject eventObj = eve;
                EventTitle.text += eventObj.Title;
                EventText.text += eventObj.Text;
            }

            StartCoroutine(ClearTextField(EventTitle, 2f));
            StartCoroutine(ClearTextField(EventText, 2f));
            StartCoroutine(ClearTextField(EventStatus, 2f));

            Debug.Log("Event ended " + CurentDate.text + " " + CurentTime.text);
        }

        public void StartDay()
        {
            Calendar.StartNewDay();

            StartButton.onClick.RemoveListener(StartDay);
        }

        public void RestartDay()
        {
            Calendar.RestartDay();
            StartButton.onClick.RemoveListener(StartDay);
        }

        public void PauseDay()
        {
            Calendar.PauseDay();
        }

        public void ContinueDay()
        {
            Calendar.ContinueDay();
        }

        private void DayOver(object source, GameClockEventObject clockObject)
        {
            print("Day is over, current time is: " + clockObject.Hour + ":" + clockObject.Minute);
            StartButton.onClick.AddListener(StartDay);
        }

        private void TimeChanged(object source, GameClockEventObject clockObject)
        {
            PrintTime();
        }

        private void Midnight(object source, GameClockEventObject clockObject)
        {
            MidnightNotification.text = "MIDNIGHT";
            StartCoroutine(ClearTextField(MidnightNotification, 1f));
        }
        private IEnumerator ClearTextField(Text textObject, float timer)
        {
            yield return new WaitForSeconds(timer);
            textObject.text = string.Empty;
        }

        public void ScaleTime(float scaleParam)
        {
            Calendar.ChangeTimeScale(scaleParam);
        }

        private void PrintCalendarGUI()
        {
            CalendarYearOutput.text = Year.ToString();

            foreach (Transform child in CalendarBody)
            {
                foreach (Transform day in child)
                    Destroy(day.gameObject);
            }

            foreach (Transform child in CalendarEventsContainer.transform)
            {
                Destroy(child.gameObject);
            }

            int counter = DateTime.DaysInMonth(Year, (int)Month);

            for (int i = 0; i < counter; i++)
            {

                int j = new int();
                j = i + 1;


                DayOfWeek dayofweek = new DateTime(Year, (int)Month, j).DayOfWeek;

                if(dayofweek != DayOfWeek.Sunday && j == 1)
                {
                    FillEmptyDays(new DateTime(Year, (int)Month, j));
                }

                Button newDay = CreateDayButton(dayofweek);
                newDay.transform.localScale = new Vector3(1, 1, 1);

                newDay.onClick.AddListener(() => ChangeDay(j));

                newDay.name = j.ToString();
                newDay.GetComponentInChildren<Text>().text = j.ToString();

                if (j == Day)
                {
                    ColorBlock cb = newDay.colors;
                    cb.normalColor = HexToColor("3AA6D0");
                    newDay.colors = cb;
                    DayOfWeek day = new DateTime(Year, (int)Month, Day).DayOfWeek;
                    var dayEvents = Calendar.GetAllEventsByDate(new DateTime(Year, (int)Month, Day));
                    foreach (GameCalendarEventObject currentEvent in dayEvents)
                    {
                        Text newEventsOnList = Instantiate(EventPrefab, CalendarEventsContainer);
                        newEventsOnList.transform.localScale = new Vector3(1, 1, 1);

                        string dateStart = "Start: " + currentEvent.DateStart.ToString() + "\n";
                        string dateEnd = "End: " + currentEvent.DateEnd.ToString() + "\n";
                        string repeatByDate ="Repeat by date: " + currentEvent.RepeatByDate + "\n";
                        string repeat ="Repeat counters \n Daily: " + currentEvent.Daily + " Weekly: " + currentEvent.Weekly + " Monthly: " + currentEvent.Monthly + " Yearly: " + currentEvent.Yearly + "\n";
                        if(DateTime.Compare(currentEvent.DateStart, currentEvent.DateEnd) == 0)
                            newEventsOnList.text = dateStart + dateEnd + repeatByDate + repeat + "Title: " + currentEvent.Title + "\n Text: " + currentEvent.Text + "\n============================";
                        else
                            if(currentEvent.DateStart.DayOfWeek == day)
                                newEventsOnList.text = dateStart + repeatByDate + repeat + "Title: " + currentEvent.Title + "\n Text: " + currentEvent.Text + "\n============================";
                            else
                            newEventsOnList.text = dateEnd + repeatByDate + repeat + "Title: " + currentEvent.Title + "\n Text: " + currentEvent.Text + "\n============================";
                    }
                }
            }
        }

        private void FillEmptyDays(DateTime date)
        {
            DateTime lastMonth = date.AddMonths(-1);
            int newMonthFirstDayOfWeek = (int)date.DayOfWeek;
            int lastMonthDays = DateTime.DaysInMonth(lastMonth.Year, (int)lastMonth.Month);

            for(int day = lastMonthDays; day > (lastMonthDays - newMonthFirstDayOfWeek); day--)
            {
                Button prevMonthDay = CreateDayButton(new DateTime(lastMonth.Year, (int)lastMonth.Month, day).DayOfWeek);
                prevMonthDay.transform.localScale = new Vector3(1, 1, 1);

                prevMonthDay.name = (day).ToString();
                prevMonthDay.GetComponentInChildren<Text>().text = (day).ToString();

                ColorBlock cb = prevMonthDay.colors;
                cb.normalColor = HexToColor("8D8D8DFF");
                prevMonthDay.colors = cb;
            }
        }

        private Button CreateDayButton(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Monday:
                    return Instantiate(DayPrefab, CalendarBodyMonday);
                case DayOfWeek.Tuesday:
                    return Instantiate(DayPrefab, CalendarBodyTuesday);
                case DayOfWeek.Wednesday:
                    return Instantiate(DayPrefab, CalendarBodyWednesday);
                case DayOfWeek.Thursday:
                    return Instantiate(DayPrefab, CalendarBodyThursday);
                case DayOfWeek.Friday:
                    return Instantiate(DayPrefab, CalendarBodyFriday);
                case DayOfWeek.Saturday:
                    return Instantiate(DayPrefab, CalendarBodySaturday);
                case DayOfWeek.Sunday:
                    return Instantiate(DayPrefab, CalendarBodySunday);
                default:
                    return Instantiate(DayPrefab, CalendarBodyMonday);
            }
        }

        Color HexToColor(string hex)
        {
            byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
            return new Color32(r, g, b, 255);
        }

        public void YearMinus()
        {
            Year--;
            PrintCalendarGUI();
        }

        public void YearPlus()
        {
            Year++;
            PrintCalendarGUI();
        }

        public void ChangeDay(int selectedDay)
        {
            Day = selectedDay;
            PrintCalendarGUI();
        }

        public void ChangeMonth()
        {
            Month = (Months)MonthSelector.value + 1;
            PrintCalendarGUI();
        }

        public void ChangeHour()
        {
            Hour = HourSelector.value;
            PrintCalendarGUI();
        }

        public void ChangeMinute()
        {
            Minute = MinuteSelector.value;
            PrintCalendarGUI();
        }

        public void ChangeActiveHours(){
            Calendar.ChangeActiveHours(ActiveStartHourDropdown.value, ActiveEndHourDropdown.value);
        }

        private void PrintTime()
        {
            CurentDate.text = Calendar.GetCurrentDate().ToString("yyyy/MM/dd");
            CurentTime.text = Calendar.GetCurrentDate().ToString("HH:mm:ss");

            if (Calendar.GetCurrentDate().Hour == Calendar.ActiveHoursEnd - 1 && Calendar.GetCurrentDate().Minute > 50)
                CurentTime.color = Color.red;
            else
                CurentTime.color = Color.white;

            DaysFromStart.text = Calendar.DaysFromStart.ToString();

        }

        public void RepeaterCheckBoxChecker()
        {
            var eventPeriod = _endTime.Subtract(_startTime);

            DailyRepeat.interactable = true;
            WeeklyRepeat.interactable = true;
            MonthlyRepeat.interactable = true;
            YearlyRepeat.interactable = true;

            if (eventPeriod.Days > 0 || RepeatByDate.isOn)
            {
                DailyRepeat.text = "0";
                DailyRepeat.interactable = false;
            }
            if (eventPeriod.Days >= 7 || RepeatByDate.isOn)
            {
                DailyRepeat.interactable = false;
                DailyRepeat.text = "0";

                WeeklyRepeat.interactable = false;
                WeeklyRepeat.text = "0";
            }
            if (eventPeriod.Days >= DateTime.DaysInMonth(_startTime.Year, _startTime.Month))
            {
                DailyRepeat.interactable = false;
                DailyRepeat.text = "0";

                WeeklyRepeat.interactable = false;
                WeeklyRepeat.text = "0";

                MonthlyRepeat.interactable = false;
                MonthlyRepeat.text = "0";
            }
            if (eventPeriod.Days >= 365)
            {
                DailyRepeat.interactable = false;
                DailyRepeat.text = "0";

                WeeklyRepeat.interactable = false;
                WeeklyRepeat.text = "0";

                MonthlyRepeat.interactable = false;
                MonthlyRepeat.text = "0";

                YearlyRepeat.interactable = false;
                YearlyRepeat.text = "0";
            }
        }
    }
}