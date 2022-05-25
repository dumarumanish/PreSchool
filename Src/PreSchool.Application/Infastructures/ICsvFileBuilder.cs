using System.Collections.Generic;

namespace PreSchool.Application.Infastructures
{
    public interface ICsvFileBuilder
    {
        byte[] ConvertToCsv<T>(IEnumerable<T> records);
    }
}
