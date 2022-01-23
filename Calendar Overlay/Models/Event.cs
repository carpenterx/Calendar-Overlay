using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Calendar_Overlay.Models
{
    public class Event : INotifyPropertyChanged
    {
        private string _name;
        public string Name
        {
            get => _name;
            set => SetField(ref _name, value);
        }
        private DateTime _startDate;
        public DateTime StartDate
        {
            get => _startDate;
            set => SetField(ref _startDate, value);
        }

        private TimeSpan _duration;
        public TimeSpan Duration
        {
            get => _duration;
            set => SetField(ref _duration, value);
        }

        private bool _isRepeatable = false;
        public bool IsRepeatable
        {
            get => _isRepeatable;
            set => SetField(ref _isRepeatable, value);
        }

        private TimeSpan _repeatInterval;
        public TimeSpan RepeatInterval
        {
            get => _repeatInterval;
            set => SetField(ref _repeatInterval, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public Event()
        {

        }

        public Event(string name, DateTime startDate, TimeSpan duration)
        {
            Name = name;
            StartDate = startDate;
            Duration = duration;
        }

        public Event(string name, DateTime startDate, TimeSpan duration, bool isRepeatable, TimeSpan repeatInterval)
        {
            Name = name;
            StartDate = startDate;
            Duration = duration;
            IsRepeatable = isRepeatable;
            RepeatInterval = repeatInterval;
        }
    }
}
