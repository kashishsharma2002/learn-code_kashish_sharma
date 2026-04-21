using System.Text.Json;
using System.Globalization;
using ETLProcessorApp.Core.Models;

namespace ETLProcessorApp.Loaders;

public sealed class JsonDataLoader : IDataLoader
{
    public string Format => "json";

    public void Load(string outputFilePath, IReadOnlyCollection<DataRecord> records)
    {
        var outputRecords = records.Select(record => new
        {
            id = record.Id,
            name = record.Name,
            value = record.Value,
            date = FormatDate(record),
            doubled_value = record.DoubledValue,
            squared_value = record.SquaredValue
        });

        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        File.WriteAllText(outputFilePath, JsonSerializer.Serialize(outputRecords, options));
    }

    private static string FormatDate(DataRecord record)
    {
        return record.FormattedDate
            ?? record.Date?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
            ?? string.Empty;
    }
}
