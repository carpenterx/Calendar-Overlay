using Calendar_Overlay.Models;
using Calendar_Overlay.Windows;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Shell;

namespace Calendar_Overlay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<object> events = new();
        private List<Event> eventsToArchive = new();
        private List<Event> currentEvents = new();

        private const string EVENTS_FILE_NAME = "events.json";
        private const string ARCHIVE_FILE_NAME = "archive.json";
        private const string APPLICATION_FOLDER = "Calendar Overlay";

        public MainWindow()
        {
            InitializeComponent();

            //events = EventsGenerator.GenerateTestEvents();
            LoadEvents();
            eventsListView.ItemsSource = events;
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MouseMoveHandler(object sender, System.Windows.Input.MouseEventArgs e)
        {
            double yPosition = e.GetPosition(this).Y;
            if (yPosition < 40)
            {
                settingsPanel.Visibility = Visibility.Visible;
            }
            else
            {
                settingsPanel.Visibility = Visibility.Hidden;
            }
        }

        private void LoadWindowProperties(object sender, EventArgs e)
        {
            this.Top = Properties.Settings.Default.WindowTop;
            this.Left = Properties.Settings.Default.WindowLeft;
            this.Height = Properties.Settings.Default.WindowHeight;
            this.Width = Properties.Settings.Default.WindowWidth;
        }

        private void ClosingHandler(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveWindowProperties();

            SaveEvents();
        }

        private void SaveWindowProperties()
        {
            Properties.Settings.Default.WindowTop = this.Top;
            Properties.Settings.Default.WindowLeft = this.Left;
            Properties.Settings.Default.WindowHeight = this.Height;
            Properties.Settings.Default.WindowWidth = this.Width;

            Properties.Settings.Default.Save();
        }

        private void SaveEvents()
        {
            string applicationDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), APPLICATION_FOLDER);
            if (!Directory.Exists(applicationDirectory))
            {
                Directory.CreateDirectory(applicationDirectory);
            }
            List<Event> eventsList = GetEvents();
            string json = JsonConvert.SerializeObject(eventsList, Formatting.Indented);
            string eventsPath = Path.Combine(applicationDirectory, EVENTS_FILE_NAME);
            File.WriteAllText(eventsPath, json);
        }

        private List<Event> GetEvents()
        {
            return events.OfType<Event>().ToList();
        }

        private void ToggleClick(object sender, RoutedEventArgs e)
        {
            // the window can be resized but you can no longer close it with the button
            // there seems to be something that overlays the close button so it doesn't always register the click
            Application.Current.MainWindow.ResizeMode = ResizeMode.CanResize;
            WindowChrome chrome = new();
            chrome.ResizeBorderThickness = new Thickness(4);
            WindowChrome.SetWindowChrome(Application.Current.MainWindow, chrome);
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            EventWindow eventWindow = new();

            if (eventWindow.ShowDialog() == true)
            {
                currentEvents.Add(eventWindow.Event);
                currentEvents.Sort((x, y) => DateTime.Compare(x.StartDate, y.StartDate));
                events = new ObservableCollection<object>(currentEvents);
                InsertHeaders();
            }
        }

        private void EditClick(object sender, RoutedEventArgs e)
        {
            if (eventsListView.SelectedItem is Event eventToEdit)
            {
                EventWindow eventWindow = new(eventToEdit);

                if (eventWindow.ShowDialog() == true)
                {
                    int editedIndex = currentEvents.IndexOf(eventToEdit);
                    currentEvents[editedIndex] = eventWindow.Event;
                    currentEvents.Sort((x, y) => DateTime.Compare(x.StartDate, y.StartDate));
                    events = new ObservableCollection<object>(currentEvents);
                    InsertHeaders();
                }
            }
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            if (eventsListView.SelectedItem is Event eventToDelete)
            {
                int deletedIndex = currentEvents.IndexOf(eventToDelete);
                currentEvents.RemoveAt(deletedIndex);
                events = new ObservableCollection<object>(currentEvents);
                InsertHeaders();
            }
        }

        private void LoadEvents()
        {
            string eventsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), APPLICATION_FOLDER, EVENTS_FILE_NAME);

            if (File.Exists(eventsPath))
            {
                if (JsonConvert.DeserializeObject<ObservableCollection<Event>>(File.ReadAllText(eventsPath)) is ObservableCollection<Event> loadedEvents)
                {
                    List<Event> eventsList = loadedEvents.ToList();
                    eventsList.Sort((x, y) => DateTime.Compare(x.StartDate, y.StartDate));
                    
                    DateTime today = DateTime.Today;
                    for (int i = 0; i < eventsList.Count; i++)
                    {
                        if (eventsList[i].StartDate < today)
                        {
                            eventsToArchive.Add(eventsList[i]);
                        }
                        else
                        {
                            currentEvents.Add(eventsList[i]);
                        }
                    }
                    events = new ObservableCollection<object>(currentEvents);
                    InsertHeaders();
                }
            }
        }

        private void InsertHeaders()
        {
            int todayIndex = events.ToList().FindIndex(IsToday);
            if (todayIndex != -1)
            {
                events.Insert(todayIndex, "Today");
            }
            int tomorrowIndex = events.ToList().FindIndex(IsTomorrow);
            if (tomorrowIndex != -1)
            {
                events.Insert(tomorrowIndex, "Tomorrow");
            }
            int upcomingIndex = events.ToList().FindIndex(IsUpcoming);
            if (upcomingIndex != -1)
            {
                events.Insert(upcomingIndex, "Upcoming");
            }
            eventsListView.ItemsSource = events;
        }

        private static bool IsToday(object obj)
        {
            if (obj is Event e)
            {
                return e.StartDate.Date == DateTime.Today;
            }
            return false;
        }

        private static bool IsTomorrow(object obj)
        {
            DateTime tomorrow = DateTime.Today.AddDays(1);
            if (obj is Event e)
            {
                return e.StartDate.Date == tomorrow;
            }
            return false;
        }

        private static bool IsUpcoming(object obj)
        {
            if (obj is Event e)
            {
                return (e.StartDate.Date - DateTime.Today).TotalDays > 2;
            }
            return false;
        }
    }
}
