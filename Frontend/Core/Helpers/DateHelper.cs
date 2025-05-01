using Microsoft.VisualBasic;
using System.Runtime.CompilerServices;

namespace Core.Helpers
{
    public static class DateHelper
    {
        public static DateTime GetStartOfWeek(DateTime date)
        {
            int diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
            return date.AddDays(-1 * diff).Date;
        }
        public static DateTime GetEndOfWeek(DateTime date)
        {
            int diff = (7 + (DayOfWeek.Sunday - date.DayOfWeek)) % 7;
            return date.AddDays(diff).Date;
        }
        public static DateTime GetStartOfMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }
        public static DateTime GetEndOfMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
        }

        public static string GetDayWeekName(this DateOnly date)
        {
            return date.DayOfWeek switch
            {
                DayOfWeek.Monday => "Poniedziałek",
                DayOfWeek.Tuesday => "Wtorek",
                DayOfWeek.Wednesday => "Środa",
                DayOfWeek.Thursday => "Czwartek",
                DayOfWeek.Friday => "Piątek",
                DayOfWeek.Saturday => "Sobota",
                DayOfWeek.Sunday => "Niedziela",
                _ => throw new ArgumentOutOfRangeException(nameof(date), date, null)
            };
        }

        public static string GetShortDayWeekName(this DateOnly date)
        {
            return date.DayOfWeek switch
            {
                DayOfWeek.Monday => "Pn",
                DayOfWeek.Tuesday => "Wt",
                DayOfWeek.Wednesday => "Śr",
                DayOfWeek.Thursday => "Cz",
                DayOfWeek.Friday => "Pt",
                DayOfWeek.Saturday => "So",
                DayOfWeek.Sunday => "Nd",
                _ => throw new ArgumentOutOfRangeException(nameof(date), date, null)
            };
        }
    }
}
