using System.Globalization;
using ETLProcessorApp.Core.Models;

namespace ETLProcessorApp.Transformers;

public sealed class DataRecordTransformer : IRecordTransformer
{
    public DataRecord Transform(DataRecord record, ProcessingConfiguration configuration)
    {
        record.Name = record.Name.ToUpperInvariant();

        if (record.Date.HasValue)
        {
            record.FormattedDate = record.Date.Value.ToString(configuration.DateFormat, CultureInfo.InvariantCulture);
        }

        return record;
    }
}
