using System;

namespace Calendar_Overlay.Models
{
    public class Event
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndtDate { get; set; }

        public Event(string name, DateTime startDate, DateTime endDate)
        {
            Name = name;
            StartDate = startDate;
            EndtDate = endDate;
        }
    }
}
