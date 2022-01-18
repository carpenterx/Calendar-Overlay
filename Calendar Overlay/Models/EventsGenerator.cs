using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar_Overlay.Models
{
    public class EventsGenerator
    {
        public List<Event> GenerateTestEvents()
        {
            List<Event> events = new();

            DateTime today = new DateTime(2022, 1, 17);
            //DateTime today1800 = new DateTime(2022, 1, 17, 18, 0, 0);
            DateTime today1800 = today.Add(new TimeSpan(18, 0, 0));
            //DateTime today1830 = new DateTime(2022, 1, 17, 18, 30, 0);
            DateTime today1830 = today.Add(new TimeSpan(18, 30, 0));
            //DateTime today1900 = new DateTime(2022, 1, 17, 19, 0, 0);
            DateTime today1900 = today.Add(new TimeSpan(19, 0, 0));

            Event event1 = new("First Event", today1800, today1830);
            Event event2 = new("Second Event", today1800, today1900);

            //DateTime tomorrow = new DateTime(today.Year, today.Month, today.Day + 1);
            DateTime tomorrow = today.AddDays(1);

            DateTime tomorrow1500 = tomorrow.Add(new TimeSpan(15, 0, 0));
            DateTime tomorrow1550 = tomorrow.Add(new TimeSpan(15, 50, 0));
            DateTime tomorrow1700 = tomorrow.Add(new TimeSpan(17, 0, 0));
            DateTime tomorrow2340 = tomorrow.Add(new TimeSpan(23, 40, 0));

            Event event3 = new("Third Event", tomorrow1500, tomorrow1550);
            Event event4 = new("Fourth Event", tomorrow1700, tomorrow2340);

            DateTime day3 = today.AddDays(3);
            DateTime day3_1520 = day3.Add(new TimeSpan(15, 20, 0));
            DateTime day3_1700 = day3.Add(new TimeSpan(17, 0, 0));

            Event event5 = new("Fifth Event", day3_1520, day3_1700);

            DateTime day4 = today.AddDays(4);
            DateTime day4_1610 = day3.Add(new TimeSpan(16, 10, 0));
            DateTime day4_1800 = day3.Add(new TimeSpan(18, 0, 0));

            Event event6 = new("Sixth Event", day4_1610, day4_1800);

            events.Add(event1);
            events.Add(event2);
            events.Add(event3);
            events.Add(event4);
            events.Add(event5);
            events.Add(event6);

            return events;
        }
    }
}
