using System.Globalization;
using ETLProcessorApp.Core.Models;

namespace ETLProcessorApp.Loaders;

public sealed class CsvDataLoader : IDataLoader
{
    public string Format => "csv";

    public void Load(string outputFilePath, IReadOnlyCollection<DataRecord> records)
    {
        var lines = new List<string>
        {
            "ID,NAME,VALUE,DATE,DOUBLED_VALUE,SQUARED_VALUE"
        };

        lines.AddRange(records.Select(record => string.Join(
            ",",
            Escape(record.Id),
            Escape(record.Name),
            Escape(FormatNumber(record.Value)),
            Escape(FormatDate(record)),
            Escape(FormatNumber(record.DoubledValue)),
            Escape(FormatNumber(record.SquaredValue)))));

        File.WriteAllLines(outputFilePath, lines);
    }

    private static string FormatNumber(double value)
    {
        return value.ToString(CultureInfo.InvariantCulture);
    }

    private static string FormatDate(DataRecord record)
    {
        return record.FormattedDate
            ?? record.Date?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
            ?? string.Empty;
    }

    private static string Escape(string value)
    {
        if (!value.Contains(',') && !value.Contains('"') && !value.Contains('\n') && !value.Contains('\r'))
        {
            return value;
        }

        return $"\"{value.Replace("\"", "\"\"")}\"";
    }
}
