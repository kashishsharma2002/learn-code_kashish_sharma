using System.Globalization;
using ETLProcessorApp.Core.Models;

namespace ETLProcessorApp.Extractors;

public sealed class CsvDataExtractor : IDataExtractor
{
    public IReadOnlyList<DataRecord> Extract(string inputFilePath, ProcessingResult result)
    {
        if (!File.Exists(inputFilePath))
        {
            File.Create(inputFilePath).Dispose();
        }

        var records = new List<DataRecord>();
        foreach (var line in File.ReadLines(inputFilePath))
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            var parts = line.Split(',');
            if (parts.Length < 3)
            {
                result.AddError($"Invalid line format: {line}");
                continue;
            }

            records.Add(new DataRecord
            {
                Id = parts[0].Trim(),
                Name = parts[1].Trim(),
                Value = double.TryParse(parts[2].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out var value)
                    ? value
                    : 0,
                Date = parts.Length >= 4
                    && DateTime.TryParse(parts[3].Trim(), CultureInfo.InvariantCulture, DateTimeStyles.None, out var date)
                        ? date
                        : null
            });
        }

        return records;
    }
}
