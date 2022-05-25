using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using PreSchool.Application.Exceptions;
using PreSchool.Data;
using PreSchool.Data.Entities;
using System.Security.Cryptography;
using System.Globalization;
using PreSchool.Infrastructure;
using PreSchool.Application.Infastructures;

namespace PreSchool.Application.Services
{
    public class CodeService : ICodeService
    {
        private readonly IApplicationDbContext _context;
        public static List<EntitiesCode> entityCodes = null;
        public CodeService(IApplicationDbContext context)
        {
            _context = context;
            // InitializeEntityCodes();

        }

        public string GetPaddedNumber(int id, int length = 3)
        {
            return id.ToString().PadLeft(length, '0');
        }


        //to generate random 8 digit code for the customer verification
        public string Get8Digits()
        {
            var bytes = new byte[4];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            uint random = BitConverter.ToUInt32(bytes, 0) % 100000000;
            return String.Format("{0:D8}", random);
        }
        //to generate random 4 digit code for the customer verification
        public int Get4DigitsRandomNumber()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }

        //to convert number into word.
        public string AmountInWords(decimal number)
        {
            // Seperate Numner and decimal number
            string s = number.ToString("0.00", CultureInfo.InvariantCulture);
            string[] parts = s.Split('.');
            int numberPart = int.Parse(parts[0]);
            int decimalPart = int.Parse(parts[1]);

            // Check if there is no decimal part
            if (decimalPart == 0)
                return $"NPR {NumberToWords(numberPart)} Only.";

            return $"NPR {NumberToWords(numberPart)} and {NumberToWords(decimalPart)} Paisa Only.";
        }
        public string NumberToWords(int number)
        {
            if (number == 0)
                return "Zero";

            if (number < 0)
                return "Minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " Million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " Thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " Hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
                var tensMap = new[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }

        //to convert hour to days.
        public string ConvertHoursToTotalDays(decimal hours)
        {
            decimal day = 0;
            decimal hour = 0;
            day = Math.Floor(hours / 24);
            hour = hours % 24;
            var dy = day > 1 ? "days" : "day";
            var hr = hour > 1 ? "hours" : "hour";
            var time = $"{day } {dy} {decimal.Round(hour, 0, MidpointRounding.AwayFromZero)} {hr}";
            return time;
        }

    }
}
