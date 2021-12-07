using Aidan.Common.Core.Interfaces.Contract;
using Aidan.Common.Core.Interfaces.Excluded;
using BrunelUni.WeatherStation.Core.Interfaces.Contract;
using BrunelUni.WeatherStation.Core.Models;
using BrunelUni.WeatherStation.Crosscutting.DIModule;
using BrunelUni.WeatherStation.DIModule;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder( args );
builder.Services
    .BindCrosscuttingLayer( )
    .BindWeatherStationServices( );
var app = builder.Build();

//TODO: upgrade common to net6 and run all initialize classes;
var service = app.Services.GetService<ILibcAdapter>( );
foreach( var initialisable in app.Services.GetServices<IInitialisable>( ) )
{
    initialisable.Initialize( );
}

app.MapGet( "/temperature", ( ITemperatureRepository temperatureRepository )
    => temperatureRepository.GetAll( ).Value );
app.MapGet("/humidity",
    ( IDHT20Service dht20Service, IDateTimeAdapter dateTimeAdapter ) => new Humidity
    {
        RelativeHumidity = dht20Service.ReadHumidity(  ).Value,
        ReadingAt = dateTimeAdapter.Now( )
    } );
app.MapGet( "/temperature/current",
        ( IDHT20Service dht20Service, IDateTimeAdapter dateTimeAdapter ) => new Temperature
        {
            Celsius = dht20Service.ReadTemperature( ).Value,
            ReadingAt = dateTimeAdapter.Now( )
        } );
app.MapGet("/humidity/current",
    ( IDHT20Service dht20Service, IDateTimeAdapter dateTimeAdapter ) => new Humidity
    {
        RelativeHumidity = dht20Service.ReadHumidity(  ).Value,
        ReadingAt = dateTimeAdapter.Now( )
    } );

app.MapGet( "/test",
    ( ) => new
    {
        test = "hi"
    } );

app.Run();