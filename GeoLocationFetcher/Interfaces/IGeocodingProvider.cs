namespace GeoLocationFetcher.Interfaces;

using GeoLocationFetcher.Models;

public interface IGeocodingProvider
{
    Task<List<LocationResult>> GetCoordinatesAsync(string location);
}
