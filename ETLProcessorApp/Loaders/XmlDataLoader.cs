using System.Globalization;
using System.Xml.Linq;
using ETLProcessorApp.Core.Models;

namespace ETLProcessorApp.Loaders;

public sealed class XmlDataLoader : IDataLoader
{
    public string Format => "xml";

    public void Load(string outputFilePath, IReadOnlyCollection<DataRecord> records)
    {
        var document = new XDocument(
            new XDeclaration("1.0", "UTF-8", null),
            new XElement(
                "records",
                records.Select(record =>
                    new XElement(
                        "record",
                        new XElement("id", record.Id),
                        new XElement("name", record.Name),
                        new XElement("value", FormatNumber(record.Value)),
                        new XElement("date", FormatDate(record)),
                        new XElement("doubled_value", FormatNumber(record.DoubledValue)),
                        new XElement("squared_value", FormatNumber(record.SquaredValue))))));

        document.Save(outputFilePath);
    }

    private static string FormatNumber(double value)
    {
        return value.ToString(CultureInfo.InvariantCulture);
    }

    private static string FormatDate(DataRecord record)
    {
        return record.FormattedDate
            ?? record.Date?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
            ?? string.Empty;
    }
}
