using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.Common.DateConverter
{
    public interface IDateConverter
    {
        ConvertedDate EngToNep(int year, int month, int day);
        ConvertedDate NepToEng(int year, int month, int day);
    }
}
