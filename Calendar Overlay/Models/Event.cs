using System;

namespace Calendar_Overlay.Models
{
    public class Event
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public TimeSpan Duration { get; set; }

        public Event()
        {

        }

        public Event(string name, DateTime startDate, TimeSpan duration)
        {
            Name = name;
            StartDate = startDate;
            Duration = duration;
        }
    }
}
