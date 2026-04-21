namespace ETLProcessorApp.Core.Models;

public class ProcessingResult
{
    public int RecordsProcessed { get; private set; }
    public int ErrorCount { get; private set; }
    public List<string> Errors { get; } = [];
    public List<DataRecord> Records { get; private set; } = [];
    public Dictionary<string, int> Statistics { get; private set; } = [];
    public bool Succeeded { get; private set; } = true;

    public void SetRecords(IEnumerable<DataRecord> records)
    {
        Records = records.ToList();
        RecordsProcessed = Records.Count;
    }

    public void AddError(string error)
    {
        Errors.Add(error);
        ErrorCount++;
    }

    public void AddValidationErrors(IEnumerable<string> validationErrors)
    {
        var errors = validationErrors.ToList();
        if (errors.Count == 0)
        {
            return;
        }

        Errors.AddRange(errors);
        ErrorCount++;
    }

    public void SetStatistics(IDictionary<string, int> statistics)
    {
        Statistics = new Dictionary<string, int>(statistics);
    }

    public void MarkFailed(string error)
    {
        Succeeded = false;
        AddError(error);
    }
}
