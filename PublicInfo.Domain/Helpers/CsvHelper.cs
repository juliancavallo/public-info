using CsvHelper;
using CsvHelper.Configuration;
using PublicInfo.Domain.Entities.Csv;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;

namespace PublicInfo.Domain.Helpers
{
    public class CsvHelper
    {
        public static IEnumerable<T> GetAllRecordsFromCsv<T>(string url) where T : CsvRecord
        {
            WebClient web = new WebClient();
            Stream stream = web.OpenRead(url);
            StreamReader reader = new StreamReader(stream, System.Text.Encoding.Latin1);

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header.ToLower(),
                TrimOptions = TrimOptions.Trim
            };
            var csvReader = new CsvReader(reader, config);
            var csvRecords = csvReader.GetRecords<T>();

            return csvRecords;
        }
    }
}
