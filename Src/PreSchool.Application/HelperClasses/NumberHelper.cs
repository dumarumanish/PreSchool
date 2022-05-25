using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application
{
    public static class NumberHelper
    {
        public static string GetPaddedNumber(int id, int length = 3)
        {
            return id.ToString().PadLeft(length, '0');
        }
    }
}
