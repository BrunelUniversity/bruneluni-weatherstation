using System;
using System.Threading.Tasks;
using Aidan.Common.Core.Interfaces.Contract;
using BrunelUni.WeatherStation.Core.Interfaces.Contract;
using BrunelUni.WeatherStation.Crosscutting.DIModule;
using BrunelUni.WeatherStation.DIModule;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder( args );
builder.Services
    .BindCrosscuttingLayer( )
    .BindWeatherStationServices( );
var app = builder.Build();

app.MapGet( "/init", ( IPollingTemperatureStateService pollingTemperatureStateService, ITemperatureEventState temperatureEventState ) =>
{
    //TODO: upgrade common to net6 and run all initialize classes
    pollingTemperatureStateService.Initialize( );
    temperatureEventState.ValueChangedEvent += ( ) => Console.WriteLine( "state changed" );
    Task.Delay( 10000 );
    return new
    {
        message = "success"
    };
} );

app.MapGet( "/temperature/current",
        ( IDHT20Service dht20Service, IDateTimeAdapter dateTimeAdapter ) => new
        {
            temperature = dht20Service.ReadTemperature( ).Value,
            time = dateTimeAdapter.Now( )
        } );
app.MapGet("/humidity/current",
    ( IDHT20Service dht20Service, IDateTimeAdapter dateTimeAdapter ) => new
    {
        humidity = dht20Service.ReadHumidity( ).Value,
        time = dateTimeAdapter.Now( )
    } );

app.Run();