namespace GeoLocationFetcher.Services;

using GeoLocationFetcher.Interfaces;
using GeoLocationFetcher.Models;

public class LocationService : ILocationService
{
    private readonly IGeocodingProvider _provider;

    public LocationService(IGeocodingProvider provider)
    {
        _provider = provider;
    }

    public async Task<List<LocationResult>> GetLocationAsync(string location)
    {
        var results = await _provider.GetCoordinatesAsync(location);

        if (results == null || results.Count == 0)
            throw new Exception("No results found.");

        return results
            .Where(r => !string.IsNullOrWhiteSpace(r.Name) && 
                        r.Name.Contains(location, System.StringComparison.OrdinalIgnoreCase))
            .GroupBy(r => r.Name)
            .Select(g => g.First())
            .OrderByDescending(r => r.Name.Equals(location, System.StringComparison.OrdinalIgnoreCase))
            .ThenByDescending(r => r.Name.StartsWith(location, System.StringComparison.OrdinalIgnoreCase))
            .ThenBy(r => r.Name)
            .ToList();
    }
}