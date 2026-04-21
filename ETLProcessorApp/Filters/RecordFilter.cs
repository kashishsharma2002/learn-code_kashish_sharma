using ETLProcessorApp.Core.Models;

namespace ETLProcessorApp.Filters;

public sealed class RecordFilter
{
    public IReadOnlyList<DataRecord> FilterByValue(IEnumerable<DataRecord> records, double minimumValue)
    {
        return records
            .Where(record => record.Value >= minimumValue)
            .ToList();
    }
}
