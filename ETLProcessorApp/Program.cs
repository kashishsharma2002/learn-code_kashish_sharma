using ETLProcessorApp.Core.Models;
using ETLProcessorApp.Extractors;
using ETLProcessorApp.Filters;
using ETLProcessorApp.Loaders;
using ETLProcessorApp.Logging;
using ETLProcessorApp.Pipeline;
using ETLProcessorApp.SampleData;
using ETLProcessorApp.Statistics;
using ETLProcessorApp.Transformers;
using ETLProcessorApp.Validators;

const string inputFilePath = "input.csv";
const string outputFilePath = "output.csv";

SampleDataGenerator.Generate(inputFilePath, 50);

var configuration = new ProcessingConfiguration(
    validateData: true,
    transformData: true,
    dateFormat: "MM/dd/yyyy",
    batchSize: 50);

var loaders = new IDataLoader[]
{
    new CsvDataLoader(),
    new JsonDataLoader(),
    new XmlDataLoader()
};

var logger = new FileEtlLogger(configuration.LogFilePath);
var pipeline = new EtlPipeline(
    new CsvDataExtractor(),
    new DataRecordValidator(),
    new DataRecordTransformer(),
    loaders,
    new StatisticsCalculator(),
    logger);

var result = pipeline.Process(inputFilePath, outputFilePath, "csv", configuration);

DisplayStatistics(result);

pipeline.Export("output.json", "json", result.Records);
pipeline.Export("output.xml", "xml", result.Records);

try
{
    pipeline.Export("output_test.json", "json", result.Records);
    pipeline.Export("output_test.xml", "xml", result.Records);
}
catch (Exception ex)
{
    Console.WriteLine($"Export error: {ex.Message}");
}

var filtered = new RecordFilter().FilterByValue(result.Records, 100);
Console.WriteLine($"\nFiltered records: {filtered.Count}");
Console.WriteLine($"\nRecords processed: {result.RecordsProcessed}");
Console.WriteLine($"Errors: {result.ErrorCount}");

static void DisplayStatistics(ProcessingResult result)
{
    Console.WriteLine("\n=== Processing Statistics ===");
    foreach (var stat in result.Statistics)
    {
        Console.WriteLine($"{stat.Key}: {stat.Value}");
    }

    if (result.Errors.Count == 0)
    {
        return;
    }

    Console.WriteLine("\n=== Errors ===");
    foreach (var error in result.Errors)
    {
        Console.WriteLine($"- {error}");
    }
}
