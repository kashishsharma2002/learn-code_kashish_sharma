namespace ETLProcessorApp.Core.Models;

public class DataRecord
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public double Value { get; set; }
    public DateTime? Date { get; set; }
    public string? FormattedDate { get; set; }

    public double DoubledValue => Value * 2;
    public double SquaredValue => Value * Value;
}
