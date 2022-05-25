
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.Common.DateConverter
{
    public class DateConverter : IDateConverter
    {
        #region Normal Variables
        private Dictionary<int, int[]> BsCalender;
        private IDictionary<int, string> WeekNames;
        private IDictionary<int, string> EnglishMonthNames;
        private IDictionary<int, string> NepaliMonthNames;
        #endregion

        #region ENG2NEP VARIABLES
        //least possible English date
        private readonly int StartingEngYear = 1944;
        private readonly int StartingEngMonth = 01;
        private readonly int StartingEngDay = 01;

        //equivalent Nepali date
        private readonly int StartingNepYear = 2000;
        private readonly int StartingNepMonth = 9;
        private readonly int StartingNepDay = 17;
        int daysInMonth = 0;
        int dayOfWeek = 7; // 1944/1/1 is saturday
        #endregion

        #region NEP2ENG VARIABLES
        //least possible nepali date
        private readonly int StartNepYear = 2000;
        private readonly int StartNepMonth = 1;
        private readonly int StartNepDay = 1;

        //equivalent english date
        private readonly int StartEngYear = 1943;
        private readonly int StartEngMonth = 4;
        private readonly int StartEngDay = 14;
        long totalNepDaysCount = 0;
        int nepDayofWeek = 4;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public DateConverter()
        {
            //Getting the Loaded Dates from DateLoader Class of helper folder
            BsCalender = LoadBsCalender();
            WeekNames = LoadWeekNames();
            EnglishMonthNames = LoadEnglishMonthNames();
            NepaliMonthNames = LoadNepaliMonthNames();
        }
        /// <summary>
        /// Convert english(AD) date to equivalent Nepali(BS) date.
        /// </summary>
        /// <param name="year">English year(Least Valid year is 1944)</param>
        /// <param name="month">Month</param>
        /// <param name="day">Day</param>
        /// <returns>Return Converted Nepali Date.</returns>
        public ConvertedDate EngToNep(int year, int month, int day)
        {
            try
            {
                //AD 2 BS convert logic
                string dayName = string.Empty;
                dayOfWeek = 7;
                string date = year + "-" + month + "-" + day;
                var totalEngDays = EngDaysBetween(DateTime.Parse("1944-01-01"), DateTime.Parse(date)); //1944-01-01 is least valid english date

                int nepYear = this.StartingNepYear;
                int nepMonth = this.StartingNepMonth;
                int nepDay = this.StartingNepDay;

                while (totalEngDays != 0)
                {
                    if (BsCalender.ContainsKey(nepYear))
                        daysInMonth = BsCalender[nepYear][nepMonth];
                    nepDay++;

                    if (nepDay > daysInMonth)
                    {
                        nepMonth++;
                        nepDay = 1;
                    }

                    if (nepMonth > 12)
                    {
                        nepYear++;
                        nepMonth = 1;
                    }

                    dayOfWeek++;
                    if (dayOfWeek > 7)
                        dayOfWeek = 1;

                    totalEngDays--;
                }

                return new ConvertedDate(nepYear, nepMonth, nepDay, GetDayOfWeek(dayOfWeek), GetNepaliMonth(nepMonth), dayOfWeek);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Convert Nepali(BS) date to equivalent English(AD) Date
        /// </summary>
        /// <param name="year">Nepali year (2000 is least supported)</param>
        /// <param name="month">Month</param>
        /// <param name="day">Day</param>
        /// <returns>Returns Converted English Date.</returns>
        public ConvertedDate NepToEng(int year, int month, int day)
        {
            //BS 2 AD convert logic
            try
            {
                nepDayofWeek = 4;
                string date = year + "-" + month + "-" + day;
                //totalNepDaysCount = NepDaysBetween(DateTime.Parse("2000-01-01"), DateTime.Parse(date));
                totalNepDaysCount = NepDaysBetween(DateTime.Parse("2000-01-01"), date);
                #region diff no of days for leap year and non leap year
                int[] daysInMonth = new int[] { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
                int[] daysInMonthOfLeapYear = new int[] { 0, 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
                #endregion

                int engYear = StartEngYear;
                int engMonth = StartEngMonth;
                int engDay = StartEngDay;

                int endDayofMonth = 0;
                //main engine 
                while (totalNepDaysCount != 0)
                {
                    if (IsLeapYear(engYear))
                        endDayofMonth = daysInMonthOfLeapYear[engMonth];
                    else
                        endDayofMonth = daysInMonth[engMonth];

                    engDay++;
                    nepDayofWeek++;
                    if (engDay > endDayofMonth)
                    {
                        engMonth++;
                        engDay = 1;
                        if (engMonth > 12)
                        {
                            engYear++;
                            engMonth = 1;
                        }
                    }

                    if (nepDayofWeek > 7)
                    {
                        nepDayofWeek = 1;
                    }

                    totalNepDaysCount--;
                }
                return new ConvertedDate(engYear, engMonth, engDay, GetDayOfWeek(nepDayofWeek), GetEnglishMonth(engMonth), nepDayofWeek);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get the total number of days between English dates.
        /// </summary>
        /// <param name="basedate">Base/Starting Date i.e.1944-01-01 (Least Valid English date)</param>
        /// <param name="currentdate">Current date</param>
        private long EngDaysBetween(DateTime basedate, DateTime currentdate)
        {
            long TotalDiffDays = 0;

            while (DateTime.Compare(basedate, currentdate) != 0)
            {
                basedate = basedate.AddDays(1);
                TotalDiffDays++;
            }
            return TotalDiffDays;
        }

        /// <summary>
        /// Get the total number of days between Nepali dates
        /// </summary>
        ///<param name="basedate">Base/Starting Date</param>
        /// <param name="currentdate">Current date</param>
        private long NepDaysBetween(DateTime basedate, string date)
        {
            string[] mydate = date.Split('-');
            long TotalDiffDays = 0;
            //if(Convert.ToInt32(dates[2]) > 32)
            //{

            //}
            for (int i = basedate.Year; i < Convert.ToInt32(mydate[0]); i++)
            {
                for (var j = 1; j <= 12; j++)
                {
                    TotalDiffDays += BsCalender[i][j]; // i year ko j month ko date
                }
            }

            for (var j = basedate.Month; j < Convert.ToInt32(mydate[1]); j++)
            {
                TotalDiffDays += BsCalender[Convert.ToInt32(mydate[0])][j];
            }

            TotalDiffDays += Convert.ToInt32(mydate[2]) - basedate.Day;

            return TotalDiffDays;
        }

        /// <summary>
        /// Check weather given year is leap year or not.
        /// </summary>
        /// <param name="year">Year to check weather leap year or not.</param>
        private Boolean IsLeapYear(int year)
        {
            if (year % 100 == 0)
                return year % 400 == 0;
            else
                return year % 4 == 0;
        }

        private Dictionary<int, int[]> LoadBsCalender()
        {
            try
            {
                BsCalender.Add(2000, new int[] { 0, 30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31 });
                BsCalender.Add(2001, new int[] { 0, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 });
                BsCalender.Add(2002, new int[] { 0, 31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30 });
                BsCalender.Add(2003, new int[] { 0, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31 });
                BsCalender.Add(2004, new int[] { 0, 30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31 });
                BsCalender.Add(2005, new int[] { 0, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 });
                BsCalender.Add(2006, new int[] { 0, 31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30 });
                BsCalender.Add(2007, new int[] { 0, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31 });
                BsCalender.Add(2008, new int[] { 0, 31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 29, 31 });
                BsCalender.Add(2009, new int[] { 0, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 });
                BsCalender.Add(2010, new int[] { 0, 31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30 });
                BsCalender.Add(2011, new int[] { 0, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31 });
                BsCalender.Add(2012, new int[] { 0, 31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30 });
                BsCalender.Add(2013, new int[] { 0, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 });
                BsCalender.Add(2014, new int[] { 0, 31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30 });
                BsCalender.Add(2015, new int[] { 0, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31 });
                BsCalender.Add(2016, new int[] { 0, 31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30 });
                BsCalender.Add(2017, new int[] { 0, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 });
                BsCalender.Add(2018, new int[] { 0, 31, 32, 31, 32, 31, 30, 30, 29, 30, 29, 30, 30 });
                BsCalender.Add(2019, new int[] { 0, 31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31 });
                BsCalender.Add(2020, new int[] { 0, 31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30 });
                BsCalender.Add(2021, new int[] { 0, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 });
                BsCalender.Add(2022, new int[] { 0, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30 });
                BsCalender.Add(2023, new int[] { 0, 31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31 });
                BsCalender.Add(2024, new int[] { 0, 31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30 });
                BsCalender.Add(2025, new int[] { 0, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 });
                BsCalender.Add(2026, new int[] { 0, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31 });
                BsCalender.Add(2027, new int[] { 0, 30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31 });
                BsCalender.Add(2028, new int[] { 0, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 });
                BsCalender.Add(2029, new int[] { 0, 31, 31, 32, 31, 32, 30, 30, 29, 30, 29, 30, 30 });
                BsCalender.Add(2030, new int[] { 0, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31 });
                BsCalender.Add(2031, new int[] { 0, 30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31 });
                BsCalender.Add(2032, new int[] { 0, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 });
                BsCalender.Add(2033, new int[] { 0, 31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30 });
                BsCalender.Add(2034, new int[] { 0, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31 });
                BsCalender.Add(2035, new int[] { 0, 30, 32, 31, 32, 31, 31, 29, 30, 30, 29, 29, 31 });
                BsCalender.Add(2036, new int[] { 0, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 });
                BsCalender.Add(2037, new int[] { 0, 31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30 });
                BsCalender.Add(2038, new int[] { 0, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31 });
                BsCalender.Add(2039, new int[] { 0, 31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30 });
                BsCalender.Add(2040, new int[] { 0, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 });
                BsCalender.Add(2041, new int[] { 0, 31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30 });
                BsCalender.Add(2042, new int[] { 0, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31 });
                BsCalender.Add(2043, new int[] { 0, 31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30 });
                BsCalender.Add(2044, new int[] { 0, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 });
                BsCalender.Add(2045, new int[] { 0, 31, 32, 31, 32, 31, 30, 30, 29, 30, 29, 30, 30 });
                BsCalender.Add(2046, new int[] { 0, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31 });
                BsCalender.Add(2047, new int[] { 0, 31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30 });
                BsCalender.Add(2048, new int[] { 0, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 });
                BsCalender.Add(2049, new int[] { 0, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30 });
                BsCalender.Add(2050, new int[] { 0, 31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31 });
                BsCalender.Add(2051, new int[] { 0, 31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30 });
                BsCalender.Add(2052, new int[] { 0, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 });
                BsCalender.Add(2053, new int[] { 0, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30 });
                BsCalender.Add(2054, new int[] { 0, 31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31 });
                BsCalender.Add(2055, new int[] { 0, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 });
                BsCalender.Add(2056, new int[] { 0, 31, 31, 32, 31, 32, 30, 30, 29, 30, 29, 30, 30 });
                BsCalender.Add(2057, new int[] { 0, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31 });
                BsCalender.Add(2058, new int[] { 0, 30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31 });
                BsCalender.Add(2059, new int[] { 0, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 });
                BsCalender.Add(2060, new int[] { 0, 31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30 });
                BsCalender.Add(2061, new int[] { 0, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31 });
                BsCalender.Add(2062, new int[] { 0, 30, 32, 31, 32, 31, 31, 29, 30, 29, 30, 29, 31 });
                BsCalender.Add(2063, new int[] { 0, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 });
                BsCalender.Add(2064, new int[] { 0, 31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30 });
                BsCalender.Add(2065, new int[] { 0, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31 });
                BsCalender.Add(2066, new int[] { 0, 31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 29, 31 });
                BsCalender.Add(2067, new int[] { 0, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 });
                BsCalender.Add(2068, new int[] { 0, 31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30 });
                BsCalender.Add(2069, new int[] { 0, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31 });
                BsCalender.Add(2070, new int[] { 0, 31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30 });
                BsCalender.Add(2071, new int[] { 0, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 });
                BsCalender.Add(2072, new int[] { 0, 31, 32, 31, 32, 31, 30, 30, 29, 30, 29, 30, 30 });
                BsCalender.Add(2073, new int[] { 0, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31 });
                BsCalender.Add(2074, new int[] { 0, 31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30 });
                BsCalender.Add(2075, new int[] { 0, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 });
                BsCalender.Add(2076, new int[] { 0, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30 });
                BsCalender.Add(2077, new int[] { 0, 31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31 });
                BsCalender.Add(2078, new int[] { 0, 31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30 });
                BsCalender.Add(2079, new int[] { 0, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 });
                BsCalender.Add(2080, new int[] { 0, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30 });
                BsCalender.Add(2081, new int[] { 0, 31, 31, 32, 32, 31, 30, 30, 30, 29, 30, 30, 30 });
                BsCalender.Add(2082, new int[] { 0, 30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 30, 30 });
                BsCalender.Add(2083, new int[] { 0, 31, 31, 32, 31, 31, 30, 30, 30, 29, 30, 30, 30 });
                BsCalender.Add(2084, new int[] { 0, 31, 31, 32, 31, 31, 30, 30, 30, 29, 30, 30, 30 });
                BsCalender.Add(2085, new int[] { 0, 31, 32, 31, 32, 30, 31, 30, 30, 29, 30, 30, 30 });
                BsCalender.Add(2086, new int[] { 0, 30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 30, 30 });
                BsCalender.Add(2087, new int[] { 0, 31, 31, 32, 31, 31, 31, 30, 30, 29, 30, 30, 30 });
                BsCalender.Add(2088, new int[] { 0, 30, 31, 32, 32, 30, 31, 30, 30, 29, 30, 30, 30 });
                BsCalender.Add(2089, new int[] { 0, 30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 30, 30 });
                BsCalender.Add(2090, new int[] { 0, 30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 30, 30 });
                BsCalender.Add(2091, new int[] { 0, 31, 31, 32, 31, 31, 31, 30, 30, 29, 30, 30, 30 });
                BsCalender.Add(2092, new int[] { 0, 30, 31, 32, 32, 31, 30, 30, 30, 29, 30, 30, 30 });
                BsCalender.Add(2093, new int[] { 0, 30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 30, 30 });
                BsCalender.Add(2094, new int[] { 0, 31, 31, 32, 31, 31, 30, 30, 30, 29, 30, 30, 30 });
                BsCalender.Add(2095, new int[] { 0, 31, 31, 32, 31, 31, 31, 30, 29, 30, 30, 30, 30 });
                BsCalender.Add(2096, new int[] { 0, 30, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30 });
                BsCalender.Add(2097, new int[] { 0, 31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 30, 30 });
                BsCalender.Add(2098, new int[] { 0, 31, 31, 32, 31, 31, 31, 29, 30, 29, 30, 29, 31 });
                BsCalender.Add(2099, new int[] { 0, 31, 31, 32, 31, 31, 31, 30, 29, 29, 30, 30, 30 });
                return BsCalender;
            }
            catch (Exception ex)
            {
                throw new Exception("Error Occured.");
            }
        }

        private Dictionary<int, string> LoadWeekNames()
        {
            return new Dictionary<int, string>
            {
                { 1, "Sunday" },
                { 2, "Monday" },
                { 3, "Tuesday" },
                { 4, "Wednesday" },
                { 5, "Thursday" },
                { 6, "Friday" },
                { 7, "Saturday" }
            };
        }

        private Dictionary<int, string> LoadEnglishMonthNames()
        {
            return new Dictionary<int, string>
            {
                { 1, "January" },
                { 2, "February" },
                { 3, "March" },
                { 4, "April" },
                { 5, "May" },
                { 6, "June" },
                { 7, "July" },
                { 8, "August" },
                { 9, "September" },
                { 10, "October" },
                { 11, "November" },
                { 12, "December" }
            };
        }

        private Dictionary<int, string> LoadNepaliMonthNames()
        {
            return new Dictionary<int, string>
            {
                { 1, "Baishakh" },
                { 2, "Jestha" },
                { 3, "Ashad" },
                { 4, "Shrawan" },
                { 5, "Bhadra" },
                { 6, "Aswin" },
                { 7, "Kartik" },
                { 8, "Mangshir" },
                { 9, "Poush" },
                { 10, "Magh" },
                { 11, "Falgun" },
                { 12, "Chaitra" }
            };
        }


        /// <summary>
        /// Will return Day of week for given day index
        /// </summary>
        /// <param name="day">1</param>
        /// <returns>Sunday</returns>
        private string GetDayOfWeek(int day)
        {
            if (day < 1 || day > 7)
                day = 1;
            return WeekNames[day];
        }

        /// <summary>
        /// Will return English month name for given month index
        /// </summary>
        /// <param name="month">2</param>
        /// <returns>February</returns>
        private string GetEnglishMonth(int month)
        {
            if (month < 1 || month > 12)
                month = 1;
            return EnglishMonthNames[month];
        }

        /// <summary>
        /// Will return Nepali month name for given month index
        /// </summary>
        /// <param name="month">12</param>
        /// <returns>Chaitra</returns>
        private string GetNepaliMonth(int month)
        {
            if (month < 1 || month > 12)
                month = 1;
            return NepaliMonthNames[month];
        }
    }

}

