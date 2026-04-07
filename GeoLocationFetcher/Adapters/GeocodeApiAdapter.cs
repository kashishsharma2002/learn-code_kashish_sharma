namespace GeoLocationFetcher.Adapter;

using System.Text.Json;
using GeoLocationFetcher.Interfaces;
using GeoLocationFetcher.Models;
using GeoLocationFetcher.Config;

public class GeocodeApiAdapter : IGeocodingProvider
{
    private readonly HttpClient _httpClient;
    private readonly AppSettings _settings;

    public GeocodeApiAdapter(AppSettings settings)
    {
        _settings = settings;
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "GeoLocationFetcher/1.0");
    }

    public async Task<List<LocationResult>> GetCoordinatesAsync(string location)
    {
        var url = BuildUrl(location);

        var json = await CallApiAsync(url);

        return ParseResponse(json);
    }

    private string BuildUrl(string location)
    {
        return $"{_settings.BaseUrl}?q={Uri.EscapeDataString(location)}&format=json";
    }

    private async Task<string> CallApiAsync(string url)
    {
        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            throw new Exception($"API call failed: {response.StatusCode}");

        return await response.Content.ReadAsStringAsync();
    }

    private List<LocationResult> ParseResponse(string json)
    {
        var results = new List<LocationResult>();

        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;

        if (root.ValueKind != JsonValueKind.Array)
            return results;

        foreach (var element in root.EnumerateArray())
        {
            double.TryParse(element.GetProperty("lat").GetString(), out double lat);
            double.TryParse(element.GetProperty("lon").GetString(), out double lon);

            results.Add(new LocationResult
            {
                Name = element.GetProperty("display_name").GetString() ?? string.Empty,
                Latitude = lat,
                Longitude = lon
            });
        }

        return results;
    }
}