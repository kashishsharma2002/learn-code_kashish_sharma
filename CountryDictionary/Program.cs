using CountryDictionary.Services;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter country code:");
        string? countryCodeInput = Console.ReadLine();

        var countryService = new CountryService();

        try
        {
            string countryName = countryService.GetCountryName(countryCodeInput);

            if (string.IsNullOrEmpty(countryName))
            {
                Console.WriteLine("Country code not found.");
            }
            else
            {
                Console.WriteLine($"Country name: {countryName}");
            }
        }
        catch (ArgumentException argEx)
        {
            Console.WriteLine($"Input error: {argEx.Message}");
        }
        catch (FileNotFoundException fileEx)
        {
            Console.WriteLine($"File error: {fileEx.Message}");
        }
        catch (InvalidOperationException jsonEx)
        {
            Console.WriteLine($"JSON parsing error: {jsonEx.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
