using System;
using Aidan.Common.Core.Interfaces.Contract;
using BrunelUni.WeatherStation.Core.Interfaces.Contract;
using BrunelUni.WeatherStation.Crosscutting.DIModule;
using BrunelUni.WeatherStation.DIModule;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder( args );
builder.Services
    .BindCrosscuttingLayer( )
    .BindWeatherStationServices( );
var app = builder.Build();

//TODO: upgrade common to net6 and run all initialize classes
app.Services.GetService<IPollingTemperatureStateService>( ).Initialize( );
app.Services.GetService<ITemperatureEventState>( ).ValueChangedEvent += ( ) => Console.WriteLine( "state changed" );

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

app.MapGet( "/test",
    ( ) => new
    {
        test = "hi"
    } );

app.Run();