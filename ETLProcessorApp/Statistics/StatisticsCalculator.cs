using ETLProcessorApp.Core.Models;

namespace ETLProcessorApp.Statistics;

public sealed class StatisticsCalculator
{
    public IDictionary<string, int> Calculate(IReadOnlyCollection<DataRecord> records, int errorCount)
    {
        var totalValue = records.Sum(record => record.Value);
        var averageValue = records.Count > 0 ? totalValue / records.Count : 0;

        return new Dictionary<string, int>
        {
            ["total_records"] = records.Count,
            ["error_count"] = errorCount,
            ["total_value"] = (int)totalValue,
            ["average_value"] = (int)averageValue
        };
    }
}
