using System;
using Aidan.Common.DependencyInjection;
using BrunelUni.WeatherStation.Core;
using BrunelUni.WeatherStation.HAL;
using Microsoft.Extensions.DependencyInjection;

namespace BrunelUni.WeatherStation.DIModule;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection BindWeatherStationServices( this IServiceCollection services ) =>
        services.BindServices( new Action[]
        {
            HALPiInitializer.Initialize,
            WeatherStationCoreInitializer.Initialize
        }, DataApplicationConstants.RootNamespace );
}