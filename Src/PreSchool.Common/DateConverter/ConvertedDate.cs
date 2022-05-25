using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.Common.DateConverter
{
    public class ConvertedDate
    {
        /// <summary>
        /// Year property
        /// </summary>
        public int Year { get; private set; }

        /// <summary>
        /// Month property
        /// </summary>
        public int Month { get; private set; }

        /// <summary>
        /// Day property
        /// </summary>
        public int Day { get; private set; }

        /// <summary>
        /// WeekDayName property
        /// </summary>
        public string WeekDayName { get; private set; }

        /// <summary>
        /// MonthName property
        /// </summary>
        public string MonthName { get; private set; }

        /// <summary>
        /// WeekDay property
        /// </summary>
        public int WeekDay { get; private set; }

        public ConvertedDate(int year, int month, int day, string weekDayName = "", string monthName = "", int weekDay = 0)
        {
            Year = year;
            Month = month;
            Day = day;
            WeekDayName = weekDayName;
            MonthName = monthName;
            WeekDay = weekDay;
        }
    }
}

