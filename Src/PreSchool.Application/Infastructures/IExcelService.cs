using PreSchool.Application.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;

namespace PreSchool.Application.Infastructures
{
    public interface IExcelService
    {
        FileDetail Export<T>(IList<T> exportData, string fileName, bool appendDateTimeInFileName = false, string sheetName = "Sheet1");
        GenericResponse<List<T>> Import<T>(IFormFile file);
    }
}