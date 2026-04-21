using ETLProcessorApp.Core.Models;

namespace ETLProcessorApp.Validators;

public interface IRecordValidator
{
    IReadOnlyList<string> Validate(DataRecord record);
}
