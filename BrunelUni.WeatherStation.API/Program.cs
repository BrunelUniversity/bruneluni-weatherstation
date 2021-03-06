using Aidan.Common.Core.Enum;
using Aidan.Common.Core.Interfaces.Contract;
using Aidan.Common.Core.Interfaces.Excluded;
using BrunelUni.WeatherStation.API;
using BrunelUni.WeatherStation.Core.Interfaces.Contract;
using BrunelUni.WeatherStation.Core.Models;
using BrunelUni.WeatherStation.Crosscutting.DIModule;
using BrunelUni.WeatherStation.DIModule;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder( args );
builder.Services
    .BindCrosscuttingLayer( )
    .BindWeatherStationServices( )
    .AddCors( );

var app = builder.Build();

//TODO: upgrade common to net6 and run all initialize classes;
foreach( var initialisable in app.Services.GetServices<IInitialisable>( ) )
{
    initialisable.Initialize( );
}

app.UseCors( x => x
    .AllowAnyHeader( )
    .AllowAnyMethod( )
    .AllowAnyOrigin( ) );

app.UseMiddleware<SimpleAuthMiddleware>( );

app.MapPost( "/ssh", ( ISecureShellService secureShellService ) =>
{
    var result = secureShellService.Activate( );
    return result.Status == OperationResultEnum.Success
        ? Results.Ok( new { message = "secure shell enabled" } )
        : Results.BadRequest( new { message = result.Msg } );
} ).AddSimpleAuthGaurd( );
app.MapDelete( "/ssh", ( ISecureShellService secureShellService ) =>
{
    var result = secureShellService.Deactivate( );
    return result.Status == OperationResultEnum.Success
        ? Results.Ok( new { message = "secure shell disabled" } )
        : Results.BadRequest( new { message = result.Msg } );
} ).AddSimpleAuthGaurd( );
app.MapGet( "/temperature", ( ITemperatureRepository temperatureRepository ) =>
    Results.Ok( temperatureRepository.GetAll( ).Value ) );
app.MapGet( "/humidity", ( IHumidityRepository humidityRepository ) =>
    Results.Ok( humidityRepository.GetAll( ).Value ) );
app.MapGet( "/temperature/current",
    ( ITemperatureEventState temperatureEventState, IDateTimeAdapter dateTimeAdapter ) => Results.Ok( new Temperature
    {
        Celsius = temperatureEventState.Value,
        ReadingAt = dateTimeAdapter.Now( )
    } ) );
app.MapGet( "/humidity/current", ( IHumidityEventState humidityEventState, IDateTimeAdapter dateTimeAdapter ) =>
    Results.Ok( new Humidity
    {
        RelativeHumidity = humidityEventState.Value,
        ReadingAt = dateTimeAdapter.Now( )
    } ) );

app.Run();