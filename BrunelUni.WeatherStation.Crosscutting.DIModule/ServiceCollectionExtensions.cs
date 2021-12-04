using System;
using Aidan.Common.Core;
using Aidan.Common.DependencyInjection;
using Aidan.Common.Utils;
using BrunelUni.WeatherStation.Core;
using Microsoft.Extensions.DependencyInjection;

namespace BrunelUni.WeatherStation.Crosscutting.DIModule;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection BindCrosscuttingLayer( this IServiceCollection serviceCollection ) =>
        serviceCollection.BindServices( new Action[]
        {
            CommonUtilsInitializer.Initialize,
            CommonInitializer.Initialize
        }, DataApplicationConstants.CrosscuttingRootNamespace );
}