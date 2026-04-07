namespace GeoLocationFetcher.Application;

using System;
using System.Threading.Tasks;
using GeoLocationFetcher.Controller;

public class App
{
    private readonly LocationController _controller;

    public App(LocationController controller)
    {
        _controller = controller;
    }

    public async Task RunAsync()
    {
        while (true)
        {
            Console.WriteLine("\n=== GeoLocation Fetcher ===");
            Console.Write("Enter location (or type 'exit' to quit): ");
            var input = Console.ReadLine() ?? string.Empty;

            if (input.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase))
                break;

            if (string.IsNullOrWhiteSpace(input))
                continue;

            try
            {
                var results = await _controller.GetLocationAsync(input);

                int index = 1;
                foreach (var res in results)
                {
                    Console.WriteLine($"\nResult {index++}:");
                    Console.WriteLine($"Name: {res.Name}");
                    Console.WriteLine($"Latitude: {res.Latitude}");
                    Console.WriteLine($"Longitude: {res.Longitude}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
