using System.Text.Json;
using CountryDictionary.Models;

namespace CountryDictionary.Services
{
    public class CountryService
    {
        private readonly string _countryJsonFilePath;

        public CountryService(string? countryJsonFilePath = null)
        {
            _countryJsonFilePath = countryJsonFilePath 
                ?? Path.Combine(AppContext.BaseDirectory, "Data", "CountryDictionary.json");
        }

        public string GetCountryName(string? countryCode)
        {
            ValidateCountryCode(countryCode);

            var countries = LoadCountriesFromFile();

            return countries.TryGetValue(countryCode!.ToUpperInvariant(), out var name) ? name : string.Empty;
        }

        private void ValidateCountryCode(string? countryCode)
        {
            if (string.IsNullOrWhiteSpace(countryCode))
                throw new ArgumentException("Country code cannot be empty.", nameof(countryCode));
        }

        private Dictionary<string, string> LoadCountriesFromFile()
        {
            if (!File.Exists(_countryJsonFilePath))
                throw new FileNotFoundException($"Country dictionary file not found: {_countryJsonFilePath}");

            try
            {
                var jsonContent = File.ReadAllText(_countryJsonFilePath);
                var countries = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonContent);

                if (countries == null)
                    throw new InvalidOperationException("Country dictionary JSON is empty or invalid.");

                return countries;
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException("Failed to parse country dictionary JSON.", ex);
            }
        }
    }
}
