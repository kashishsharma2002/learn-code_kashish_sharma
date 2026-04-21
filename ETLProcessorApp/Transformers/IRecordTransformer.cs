using ETLProcessorApp.Core.Models;

namespace ETLProcessorApp.Transformers;

public interface IRecordTransformer
{
    DataRecord Transform(DataRecord record, ProcessingConfiguration configuration);
}
