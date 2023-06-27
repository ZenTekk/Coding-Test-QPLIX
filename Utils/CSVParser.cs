using System.Collections.Generic;
using System.Linq;
using System.Text;
using CsvHelper;
using System.IO;
using CsvHelper.Configuration;
using System.Globalization;

namespace Coding_Test_QPLIX
{
    internal static class CSVParser
    {
        internal static IList<TTarget> Parse<TTarget>(string filepath, CultureInfo cultureInfo = null)
        {
            cultureInfo = cultureInfo ?? CultureInfo.InvariantCulture;
            var configuration = new CsvConfiguration(cultureInfo)
            {
                Delimiter = ";"
            };

            using (var filestream = File.Open(filepath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var streamReader = new StreamReader(filestream, Encoding.UTF8))
                using (var csvReader = new CsvReader(streamReader, configuration))
                {
                    var targetData = csvReader.GetRecords<TTarget>().ToList();
                    return targetData;
                }
            }
        }
    }
}
