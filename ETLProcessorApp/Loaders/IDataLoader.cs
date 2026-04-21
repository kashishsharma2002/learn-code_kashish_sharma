using ETLProcessorApp.Core.Models;

namespace ETLProcessorApp.Loaders;

public interface IDataLoader
{
    string Format { get; }

    void Load(string outputFilePath, IReadOnlyCollection<DataRecord> records);
}
