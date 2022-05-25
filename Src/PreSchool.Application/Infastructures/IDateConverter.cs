using System;
using System.Collections.Generic;
using System.Text;
using PreSchool.Application.Models;

namespace PreSchool.Application.Infastructures
{
    public interface IDateConverter
    {
        ConvertedDate EngToNep(int year, int month, int day);
        ConvertedDate NepToEng(int year, int month, int day);
    }
}
