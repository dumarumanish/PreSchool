using CsvHelper.Configuration;
using System.Globalization;

namespace PreSchool.Infrastructure.Files.Maps
{
    public class CsvFileMap<T> : ClassMap<T>
    {
        public CsvFileMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
          //  Map(m => m.Done).ConvertUsing(c => c.Done ? "Yes" : "No");
        }
    }
}
