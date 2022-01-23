using Calendar_Overlay.Models;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Calendar_Overlay.Windows
{
    /// <summary>
    /// Interaction logic for EventWindow.xaml
    /// </summary>
    public partial class EventWindow : Window
    {
        public Event Event { get; set; } = new();

        public EventWindow()
        {
            InitializeComponent();

            PopulateDayComboBoxes();
            PopulateHourComboBoxes();
            PopulateMinutesComboBoxes();
            SelectComboboxValues();

            DataContext = Event;
        }

        public EventWindow(Event eventToEdit)
        {
            InitializeComponent();

            PopulateDayComboBoxes();
            PopulateHourComboBoxes();
            PopulateMinutesComboBoxes();

            titleLabel.Content = "Edit Event";
            addBtn.Content = "Edit";

            Event = eventToEdit;
            DataContext = Event;

            nameTxt.Text = Event.Name;
            startDate.SelectedDate = Event.StartDate;
            startHour.SelectedValue = Event.StartDate.Hour;
            startMinutes.SelectedValue = Event.StartDate.Minute;
            DateTime endDateTime = Event.StartDate.Add(Event.Duration);
            endHour.SelectedValue = endDateTime.Hour;
            endMinutes.SelectedValue = endDateTime.Minute;

            if (Event.IsRepeatable)
            {
                repeatableCheckbox.IsChecked = true;
            }
            else
            {
                repeatableCheckbox.IsChecked = false;
            }
            repeatDays.SelectedValue = Event.RepeatInterval.Days;
            repeatHours.SelectedValue = Event.RepeatInterval.Hours;
            repeatMinutes.SelectedValue = Event.RepeatInterval.Minutes;
        }

        private void PopulateDayComboBoxes()
        {
            for (int i = 0; i <= 20; i++)
            {
                repeatDays.Items.Add(new KeyValuePair<string, int>(GetPaddedString(i), i));
            }
        }

        private void PopulateHourComboBoxes()
        {
            for (int i = 0; i < 24; i++)
            {
                startHour.Items.Add(new KeyValuePair<string, int>(GetPaddedString(i), i));
                endHour.Items.Add(new KeyValuePair<string, int>(GetPaddedString(i), i));
                repeatHours.Items.Add(new KeyValuePair<string, int>(GetPaddedString(i), i));
            }
        }

        private void PopulateMinutesComboBoxes()
        {
            for (int i = 0; i < 60; i += 5)
            {
                startMinutes.Items.Add(new KeyValuePair<string, int>(GetPaddedString(i), i));
                endMinutes.Items.Add(new KeyValuePair<string, int>(GetPaddedString(i), i));
                repeatMinutes.Items.Add(new KeyValuePair<string, int>(GetPaddedString(i), i));
            }
        }

        private void SelectComboboxValues()
        {
            startHour.SelectedIndex = 0;
            endHour.SelectedIndex = 0;
            startMinutes.SelectedIndex = 0;
            endMinutes.SelectedIndex = 0;

            repeatDays.SelectedIndex = 0;
            repeatHours.SelectedIndex = 0;
            repeatMinutes.SelectedIndex = 0;
        }

        private static string GetPaddedString(int number)
        {
            if (number < 10)
            {
                return $"0{number}";
            }
            else
            {
                return number.ToString();
            }
        }

        public void AddClick(object sender, RoutedEventArgs e)
        {
            if (startDate.SelectedDate is DateTime start)
            {
                DateTime startingDate = start.Add(new TimeSpan((int)startHour.SelectedValue, (int)startMinutes.SelectedValue, 0));
                TimeSpan duration = GetDuration((int)startHour.SelectedValue, (int)startMinutes.SelectedValue, (int)endHour.SelectedValue, (int)endMinutes.SelectedValue);
                bool isRepeatable = repeatableCheckbox.IsChecked == true ? true : false;
                TimeSpan repeatInterval = new TimeSpan((int)repeatDays.SelectedValue, (int)repeatHours.SelectedValue, (int)repeatMinutes.SelectedValue, 0);
                Event = new(nameTxt.Text, startingDate, duration, isRepeatable, repeatInterval);
                GetWindow(this).DialogResult = true;
                GetWindow(this).Close();
            }
        }

        private static TimeSpan GetDuration(int startH, int startMin, int endH, int endMin)
        {
            return TimeSpan.FromMinutes( ((endH - startH) * 60) + (endMin - startMin));
        }
    }
}
