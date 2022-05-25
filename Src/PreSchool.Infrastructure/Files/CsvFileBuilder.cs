using CsvHelper;
using PreSchool.Application.Infastructures;
using PreSchool.Infrastructure.Files.Maps;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace PreSchool.Infrastructure.Files
{
    public class CsvFileBuilder : ICsvFileBuilder
    {
        public byte[] ConvertToCsv<T>(IEnumerable<T> records)
        {
            using var memoryStream = new MemoryStream();
            using (var streamWriter = new StreamWriter(memoryStream))
            {
                using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

                csvWriter.Configuration.RegisterClassMap<CsvFileMap<T>>();
                csvWriter.WriteRecords(records);
            }

            return memoryStream.ToArray();
        }
    }
}
