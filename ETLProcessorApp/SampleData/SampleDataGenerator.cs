namespace ETLProcessorApp.SampleData;

public static class SampleDataGenerator
{
    public static void Generate(string filePath, int recordCount)
    {
        var lines = new List<string>();
        var random = new Random();

        for (var i = 1; i <= recordCount; i++)
        {
            var id = $"ID{i:D4}";
            var name = $"Item{i}";
            var value = random.Next(10, 1000);
            var date = DateTime.Now.AddDays(-random.Next(0, 365));

            lines.Add($"{id},{name},{value},{date:yyyy-MM-dd}");
        }

        File.WriteAllLines(filePath, lines);
        Console.WriteLine($"Generated {recordCount} sample records in {filePath}");
    }
}
