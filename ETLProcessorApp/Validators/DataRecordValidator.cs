using ETLProcessorApp.Core.Models;

namespace ETLProcessorApp.Validators;

public sealed class DataRecordValidator : IRecordValidator
{
    public IReadOnlyList<string> Validate(DataRecord record)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(record.Id))
        {
            errors.Add("Record missing ID");
        }

        if (string.IsNullOrWhiteSpace(record.Name))
        {
            errors.Add($"Record {record.Id} missing name");
        }

        if (record.Value <= 0)
        {
            errors.Add($"Record {record.Id} has invalid value");
        }

        return errors;
    }
}
