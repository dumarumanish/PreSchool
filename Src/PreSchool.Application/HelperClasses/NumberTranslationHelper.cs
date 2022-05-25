using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.HelperClasses
{
    public static class NumberTranslationHelper
    {
        public static string GetNepaliNumber(Object number)
        {
            if (number == null)
                return null;
            try
            {
                var nepaliNumber = "";
                foreach (char digit in number.ToString().ToCharArray())
                {
                    nepaliNumber += GetNepaliDigit(digit);
                }
                return nepaliNumber;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static string GetNepaliDigit(char number)
        {
            switch (number)
            {
                case '0': return "०";
                case '1': return "१";
                case '2': return "२";
                case '3': return "३";
                case '4': return "४";
                case '5': return "५";
                case '6': return "६";
                case '7': return "७";
                case '8': return "८";
                case '9': return "९";
                default:
                    return number.ToString();
            }
        }
    }
}
