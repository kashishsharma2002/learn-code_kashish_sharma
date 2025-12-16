using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace countryDictionary
{
    public class CountryFinder
    {
        static string GetCountryName(string? countryCode)
        {
            string filePath = Path.Combine(AppContext.BaseDirectory, "countryDictionary.json");

            if (!File.Exists(filePath))
            {
                return "Country data file not found.";
            }
            string json = File.ReadAllText(filePath);

            var countryData = JsonSerializer.Deserialize<Dictionary<string, string>>(json);

            if (!string.IsNullOrWhiteSpace(countryCode) &&
                countryData != null &&
                countryData.ContainsKey(countryCode.ToUpper()))
            {
                return countryData[countryCode.ToUpper()];
            }

            return "";
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Enter the country code to get the country name:");
            string? input = Console.ReadLine();

            string countryName = GetCountryName(input);
            if (string.IsNullOrEmpty(countryName))
            {
                Console.WriteLine("Country code not found.");
            }
            else
            {
                Console.WriteLine($"The Country name is {countryName}");
            }

        }
    }
}
