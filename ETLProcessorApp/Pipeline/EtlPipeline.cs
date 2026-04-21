using ETLProcessorApp.Core.Models;
using ETLProcessorApp.Extractors;
using ETLProcessorApp.Loaders;
using ETLProcessorApp.Logging;
using ETLProcessorApp.Statistics;
using ETLProcessorApp.Transformers;
using ETLProcessorApp.Validators;

namespace ETLProcessorApp.Pipeline;

public sealed class EtlPipeline
{
    private readonly IDataExtractor extractor;
    private readonly IRecordValidator validator;
    private readonly IRecordTransformer transformer;
    private readonly IReadOnlyDictionary<string, IDataLoader> loaders;
    private readonly StatisticsCalculator statisticsCalculator;
    private readonly IEtlLogger logger;

    public EtlPipeline(
        IDataExtractor extractor,
        IRecordValidator validator,
        IRecordTransformer transformer,
        IEnumerable<IDataLoader> loaders,
        StatisticsCalculator statisticsCalculator,
        IEtlLogger logger)
    {
        this.extractor = extractor;
        this.validator = validator;
        this.transformer = transformer;
        this.loaders = loaders.ToDictionary(loader => loader.Format, StringComparer.OrdinalIgnoreCase);
        this.statisticsCalculator = statisticsCalculator;
        this.logger = logger;
    }

    public ProcessingResult Process(
        string inputFilePath,
        string outputFilePath,
        string outputFormat,
        ProcessingConfiguration configuration)
    {
        var result = new ProcessingResult();

        try
        {
            logger.Log("Starting data processing");
            logger.Log($"Reading input file: {inputFilePath}");

            var records = extractor.Extract(inputFilePath, result).ToList();
            logger.Log($"Extracted {records.Count} records");

            if (configuration.ValidateData)
            {
                logger.Log("Validating data");
                records = Validate(records, result);
                logger.Log($"Validation complete. {records.Count} valid records");
            }

            if (configuration.TransformData)
            {
                logger.Log("Transforming data");
                records = records
                    .Select(record => transformer.Transform(record, configuration))
                    .ToList();
                logger.Log("Transformation complete");
            }

            var statistics = statisticsCalculator.Calculate(records, result.ErrorCount);
            result.SetStatistics(statistics);
            result.SetRecords(records);
            logger.Log($"Statistics calculated: {statistics.Count} metrics");

            Export(outputFilePath, outputFormat, records);
            logger.Log($"Output written. {result.RecordsProcessed} records processed");

            Console.WriteLine("Processing complete!");
        }
        catch (Exception ex)
        {
            result.MarkFailed($"Fatal error: {ex.Message}");
            logger.Log($"FATAL ERROR: {ex.Message}");
            Console.WriteLine($"Processing failed: {ex.Message}");
        }
        finally
        {
            logger.Flush();
        }

        return result;
    }

    public void Export(string outputFilePath, string outputFormat, IEnumerable<DataRecord> records)
    {
        if (!loaders.TryGetValue(outputFormat, out var loader))
        {
            throw new ArgumentException($"Unsupported format: {outputFormat}", nameof(outputFormat));
        }

        logger.Log($"Writing {outputFormat.ToUpperInvariant()} output to: {outputFilePath}");
        loader.Load(outputFilePath, records.ToList());
        logger.Flush();
    }

    private List<DataRecord> Validate(IEnumerable<DataRecord> records, ProcessingResult result)
    {
        var validRecords = new List<DataRecord>();

        foreach (var record in records)
        {
            var validationErrors = validator.Validate(record);
            if (validationErrors.Count == 0)
            {
                validRecords.Add(record);
                continue;
            }

            result.AddValidationErrors(validationErrors);

            foreach (var validationError in validationErrors)
            {
                logger.Log($"ERROR: {validationError}");
            }
        }

        return validRecords;
    }
}
