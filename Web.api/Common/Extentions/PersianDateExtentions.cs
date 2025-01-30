using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extentions
{
    public static class PersianDateExtentions
    {
        public static string ToPersianDate(this DateOnly inputDate)
        {
            var date = DateTime.Parse(inputDate.ToString());
            PersianCalendar calendar = new();

            var day = calendar.GetDayOfMonth(date);
            var month = calendar.GetMonth(date);
            var year = calendar.GetYear(date);

            return $"{year}/{month}/{day}";
        }

        public static int GetDayOfWeek(this DateTime inputDate)
        {
            var gregorianDay = (int)inputDate.DayOfWeek;
            var persianDay = gregorianDay > 5 ? gregorianDay - 5 : gregorianDay + 2;

            return persianDay;
        }

        public static int GetDayOfMonth(this DateTime inputDate)
        {
            PersianCalendar calendar = new();

            return calendar.GetDayOfMonth(inputDate);
        }

        public static int GetDayOfYear(this DateTime inputDate)
        {
            PersianCalendar calendar = new();

            return calendar.GetDayOfYear(inputDate);
        }
    }
}
