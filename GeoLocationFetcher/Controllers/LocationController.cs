namespace GeoLocationFetcher.Controller;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GeoLocationFetcher.Interfaces;
using GeoLocationFetcher.Validation;
using GeoLocationFetcher.Models;

public class LocationController
{
    private readonly ILocationService _service;

    public LocationController(ILocationService service)
    {
        _service = service;
    }

    public async Task<List<LocationResult>> GetLocationAsync(string input)
    {
        if (!InputValidator.IsValid(input))
            throw new ArgumentException("Invalid input. Please enter a valid location.");

        return await _service.GetLocationAsync(input);
    }
}