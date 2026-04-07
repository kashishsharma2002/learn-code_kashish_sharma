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
            .Where(result =>
                !string.IsNullOrWhiteSpace(result.Name) &&
                result.Name.Contains(location, StringComparison.OrdinalIgnoreCase))
            .GroupBy(result => result.Name)
            .Select(group => group.First()) 
            .OrderByDescending(result =>
                result.Name.Equals(location, StringComparison.OrdinalIgnoreCase))
            .ThenByDescending(result =>
                result.Name.StartsWith(location, StringComparison.OrdinalIgnoreCase))
            .ThenBy(result => result.Name)   
            .ToList();
    }
}