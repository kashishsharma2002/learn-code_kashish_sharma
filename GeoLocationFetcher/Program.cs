using Microsoft.Extensions.Configuration;
using GeoLocationFetcher.Config;
using GeoLocationFetcher.Adapter;
using GeoLocationFetcher.Services;
using GeoLocationFetcher.Controller;
using GeoLocationFetcher.Application;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var settings = config.GetSection("GeocodingApi").Get<AppSettings>();

if (settings == null || string.IsNullOrEmpty(settings.BaseUrl))
{
    Console.WriteLine("Base URL missing in configuration.");
    return;
}

var adapter    = new GeocodeApiAdapter(settings);
var service    = new LocationService(adapter);
var controller = new LocationController(service);
var app        = new App(controller);

await app.RunAsync();