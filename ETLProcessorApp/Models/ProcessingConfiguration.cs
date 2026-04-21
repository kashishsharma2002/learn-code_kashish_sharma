namespace ETLProcessorApp.Core.Models;

public class ProcessingConfiguration
{
    public ProcessingConfiguration()
    {
    }

    public ProcessingConfiguration(
        bool validateData = true,
        bool transformData = true,
        string dateFormat = "yyyy-MM-dd",
        int batchSize = 100,
        string logFilePath = "processing.log")
    {
        ValidateData = validateData;
        TransformData = transformData;
        DateFormat = dateFormat;
        BatchSize = batchSize;
        LogFilePath = logFilePath;
    }

    public bool ValidateData { get; set; } = true;
    public bool TransformData { get; set; } = true;
    public string DateFormat { get; set; } = "yyyy-MM-dd";
    public int BatchSize { get; set; } = 100;
    public string LogFilePath { get; set; } = "processing.log";
}
