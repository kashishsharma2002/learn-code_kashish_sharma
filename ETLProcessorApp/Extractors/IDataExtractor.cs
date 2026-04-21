using ETLProcessorApp.Core.Models;

namespace ETLProcessorApp.Extractors;

public interface IDataExtractor
{
    IReadOnlyList<DataRecord> Extract(string inputFilePath, ProcessingResult result);
}
