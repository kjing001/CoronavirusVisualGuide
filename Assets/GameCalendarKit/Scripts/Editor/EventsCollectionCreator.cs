using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GameCalendarKit.Helpers
{
    /// <summary>  
    ///  EventsObjectCreator is a partial the successor of EditorWindow and provides the necessary set of methods for: creating new event collection, add new event in current collection.
    ///  <para>
    ///    EventsObjectCreator have a partial keyword give you a possibility to extend current class and create your own realization.
    ///  </para>
    ///  <para>
    ///    more about partial is here - https://msdn.microsoft.com/en-us//library/wa80x488.aspx
    ///  </para>
    /// </summary> 
    public partial class EventsCollectionCreator : EditorWindow
    {
        public static EventsCollectionCreator Window;

        public EventsCollection EventsConfigObject;
        public string CollectionName;

        public int Year = DateTime.Now.Year;
        public Months Month = Months.January;
        public int Day = 1;
        public int Hour = 0;
        public int Minute = 0;

        public string Title = string.Empty;
        public string Text = string.Empty;

        public Boolean RepeatByDate;

        public int Daily = 0;
        public int Weekly = 0;
        public int Monthly = 0;
        public int Yearly = 0;

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

        public string[] Days;

        public int toolbarInt = 0;
        public string[] toolbarStrings = new string[] { "Prefernces", "Calendar", "Events" };

        private EventsCollection _events;
        private Editor _editor;


        [MenuItem("Assets/Game Calendar Kit/Event Collection Creator")]
        public static void Init()
        {
            Window = GetWindow<EventsCollectionCreator>(false, "EventsCollectionCreator", true);
            Window.Show();
        }

        [MenuItem("Assets/Game Calendar Kit/Help Page")]
        public static void OpenHelpPage()
        {
            Application.OpenURL("https://github.com/Gravideots/GameCalendarKit/wiki/GameCalendarKit");
        }

        [MenuItem("Assets/Game Calendar Kit/Bug report")]
        public static void BugReportPage()
        {
            Application.OpenURL("https://github.com/Gravideots/GameCalendarKit/issues");
        }

        public void OnGUI()
        {
            toolbarInt = GUILayout.Toolbar(toolbarInt, toolbarStrings);

            switch (toolbarInt)
            {
                case 0:
                    ShowPrefeences();
                    break;
                case 1:
                    ShowCalendar();
                    break;
                case 2:
                    ShowEvents();
                    break;
            }

        }

        private void Update()
        {
            if (EventsConfigObject != null)
            {
                _editor = Editor.CreateEditor(EventsConfigObject);
                _events = (EventsCollection)_editor.target;

                DateTime iDay = DateTime.Now;
                Days = new string[DateTime.DaysInMonth(Year, (int)Month)];

                for (int i = 0; i < DateTime.DaysInMonth(Year, (int)Month); i++)
                {
                    iDay = new DateTime(Year, (int)Month, i + 1);
                    Days[i] = iDay.ToString("dd");
                }
            }

        }

        //partial methods 
        partial void CreateNewEvent();
        partial void ShowPrefeences();
        partial void ShowCalendar();
        partial void ShowEvents();

        /// <summary>  
        ///  ShowPrefeences is method to draw Object fields, collection name, and button "Create new collection".
        /// </summary> 
        partial void ShowPrefeences()
        {
            EventsConfigObject = (EventsCollection)EditorGUILayout.ObjectField(EventsConfigObject, typeof(EventsCollection), true);
            CollectionName = EditorGUILayout.TextField(CollectionName);


            if (GUILayout.Button("Create new collection"))
            {
                EventsConfigObject = (EventsCollection)ScriptableObjectUtility.CreateAsset<EventsCollection>(CollectionName);
            }

        }

        /// <summary>  
        ///  ShowCalendar is method to draw calendar and controlls to set info on new event.
        /// </summary> 
        partial void ShowCalendar()
        {
            if (EventsConfigObject != null)
            {

                GUILayout.BeginVertical("Box", GUILayout.MaxWidth(400));
                EditorGUILayout.BeginHorizontal();

                if (GUILayout.Button("-", GUILayout.Width(30)))
                    Year--;
                EditorGUILayout.LabelField("Year: " + Year, GUILayout.MaxWidth(80));
                if (GUILayout.Button("+", GUILayout.Width(30)))
                    Year++;

                Month = (Months)EditorGUILayout.EnumPopup("Month: ", Month, GUILayout.MaxWidth(250));

                EditorGUILayout.EndHorizontal();
                Day = GUILayout.SelectionGrid(Day, Days, 4);

                EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Hour: " + Hour);
                    EditorGUILayout.LabelField("Minute: " + Minute);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                    Hour = EditorGUILayout.IntSlider(Hour, 0, 23);
                    Minute = EditorGUILayout.IntSlider(Minute, 0, 59);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                    RepeatByDate = EditorGUILayout.Toggle("Repeat by date?", RepeatByDate);
                EditorGUILayout.EndHorizontal();

                if (RepeatByDate)
                {
                    EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Months " + Monthly);
                        EditorGUILayout.LabelField("Years " + Yearly);
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.BeginHorizontal();
                        Monthly = EditorGUILayout.IntField(Monthly);
                        Yearly = EditorGUILayout.IntField(Yearly);
                    EditorGUILayout.EndHorizontal();
                }
                if(!RepeatByDate)
                {
                    EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.BeginVertical();
                            EditorGUILayout.LabelField("Days " + Daily);
                            Daily = EditorGUILayout.IntField(Daily);
                        GUILayout.EndVertical();
                        EditorGUILayout.BeginVertical();
                            EditorGUILayout.LabelField("Weeks " + Weekly);
                            Weekly = EditorGUILayout.IntField(Weekly);
                        GUILayout.EndVertical();
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.BeginVertical();
                            EditorGUILayout.LabelField("Months " + Monthly);
                            Monthly = EditorGUILayout.IntField(Monthly);
                        GUILayout.EndVertical();
                        EditorGUILayout.BeginVertical();
                            EditorGUILayout.LabelField("Years " + Yearly);
                            Yearly = EditorGUILayout.IntField(Yearly);
                        GUILayout.EndVertical();
                    EditorGUILayout.EndHorizontal();
                }
                GUILayout.EndVertical();


                //Aditional place for custom arguments
                GUILayout.BeginVertical("Box", GUILayout.MaxWidth(400));

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Title: " + Title);
                EditorGUILayout.LabelField("Text: " + Text);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();

                Title = EditorGUILayout.TextField(Title);
                Text = EditorGUILayout.TextField(Text);

                EditorGUILayout.EndHorizontal();
                GUILayout.EndVertical();

                if (_startDate != DateTime.MinValue)
                {
                    EditorGUILayout.LabelField("Select end date");
                    if (GUILayout.Button("Create new event", GUILayout.MaxWidth(400)))
                    {
                        CreateNewEvent();
                    }
                }
                else
                {
                    EditorGUILayout.LabelField("Select start date");
                    if (GUILayout.Button("Save date", GUILayout.MaxWidth(400)))
                    {
                        _startDate = new DateTime(Year, (int)Month, int.Parse(Days[Day]), Hour, Minute, 0);
                    }
                }
            }

        }

        /// <summary>  
        ///  ShowEvents is method to show all created event and possibility to delete it.
        /// </summary> 
        partial void ShowEvents()
        {
            if (_events != null)
            {
                List<GameCalendarEventObject> eventsList = _events.Events;

                GUILayout.BeginVertical("Box", GUILayout.MaxWidth(500));

                foreach (GameCalendarEventObject curentEvent in eventsList)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Start: " + curentEvent.DateStart.ToString());
                    EditorGUILayout.LabelField("End: " + curentEvent.DateEnd.ToString());
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Title: " + curentEvent.Title);
                    EditorGUILayout.LabelField("Text: " + curentEvent.Text);
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.Separator();
                    EditorGUILayout.EndHorizontal();

                    if (GUILayout.Button("Remove event"))
                    {
                        RemoveEvent(curentEvent);
                        break;
                    }
                }

                GUILayout.EndVertical();
            }
        }

        /// <summary>  
        ///  CreateNewEvent is method for creating new event.
        ///  <para>
        ///    CreateNewEvent have a partial keyword give you a possibility to extend current method and create your own realization.
        ///  </para>
        /// </summary> 
        partial void CreateNewEvent()
        {
            DateTime endDate = new DateTime(Year, (int)Month, int.Parse(Days[Day]), Hour, Minute, 0);

            _events.Events.Add(new GameCalendarEventObject(_startDate, endDate, Title, Text, Daily, Weekly, Monthly, Yearly, RepeatByDate));

            _startDate = DateTime.MinValue;

            EditorUtility.SetDirty(_events);
            AssetDatabase.SaveAssets();
        }

        void RemoveEvent(GameCalendarEventObject curentEvent)
        {
            _events.Events.Remove(curentEvent);

            EditorUtility.SetDirty(_events);
            AssetDatabase.SaveAssets();
        }
    }
}