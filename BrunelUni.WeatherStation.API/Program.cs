using Aidan.Common.Core.Enum;
using Aidan.Common.Core.Interfaces.Contract;
using Aidan.Common.Core.Interfaces.Excluded;
using Aidan.Common.Utils.Web;
using BrunelUni.WeatherStation.Core.Interfaces.Contract;
using BrunelUni.WeatherStation.Core.Models;
using BrunelUni.WeatherStation.Crosscutting.DIModule;
using BrunelUni.WeatherStation.DIModule;
using Microsoft.AspNetCore.Builder;
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

app.MapPost( "/ssh-server", ( ISecureShellService secureShellService, MvcAdapter mvcAdapter ) =>
    secureShellService.Activate( ).Status == OperationResultEnum.Success
        ? mvcAdapter.Success( "secure shell enabled" )
        : mvcAdapter.BadRequestError( "secure shell was not enabled" ) );
app.MapDelete( "/ssh-server", ( ISecureShellService secureShellService, MvcAdapter mvcAdapter ) =>
    secureShellService.Deactivate( ).Status == OperationResultEnum.Success
        ? mvcAdapter.Success( "secure shell disabled" )
        : mvcAdapter.BadRequestError( "secure shell was not disabled" ) );
app.MapGet( "/temperature", ( ITemperatureRepository temperatureRepository ) =>
    temperatureRepository.GetAll( ).Value );
app.MapGet( "/humidity", ( IHumidityRepository humidityRepository ) =>
    humidityRepository.GetAll( ).Value );
app.MapGet( "/temperature/current",
    ( ITemperatureEventState temperatureEventState, IDateTimeAdapter dateTimeAdapter ) => new Temperature
    {
        Celsius = temperatureEventState.Value,
        ReadingAt = dateTimeAdapter.Now( )
    } );
app.MapGet( "/humidity/current", ( IHumidityEventState humidityEventState, IDateTimeAdapter dateTimeAdapter ) =>
    new Humidity
    {
        RelativeHumidity = humidityEventState.Value,
        ReadingAt = dateTimeAdapter.Now( )
    } );

app.Run();