using System;
using Aidan.Common.DependencyInjection;
using BrunelUni.WeatherStation.Core;
using Microsoft.Extensions.DependencyInjection;

namespace BrunelUni.WeatherStation.HAL.DIModule;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection BindHardwareLayer( this IServiceCollection services ) =>
        services.BindServices( new Action[]
        {
            HALPiInitializer.Initialize,
            WeatherStationCoreInitializer.Initialize
        }, DataApplicationConstants.HardwareRootNamespace );
}