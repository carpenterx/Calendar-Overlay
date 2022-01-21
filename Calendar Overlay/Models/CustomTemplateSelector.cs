using System;
using System.Windows;
using System.Windows.Controls;

namespace Calendar_Overlay.Models
{
    public class CustomTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DefaultTemplate { get; set; }
        public DataTemplate UpcomingTemplate { get; set; }
        public DataTemplate TitleTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item,
                   DependencyObject container)
        {
            if (item is string)
            {
                return TitleTemplate;
            }
            else if(item is Event ev)
            {
                if ((ev.StartDate.Date - DateTime.Today).TotalDays >= 2)
                {
                    return UpcomingTemplate;
                }
            }

            return DefaultTemplate;
        }
    }
}
