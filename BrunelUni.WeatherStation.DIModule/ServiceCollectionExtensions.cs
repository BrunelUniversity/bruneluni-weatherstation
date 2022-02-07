using System;
using Aidan.Common.Core.Interfaces.Contract;
using Aidan.Common.DependencyInjection;
using Aidan.Common.Utils.Web;
using BrunelUni.WeatherStation.Core;
using BrunelUni.WeatherStation.Core.Interfaces.Contract;
using BrunelUni.WeatherStation.DAL;
using BrunelUni.WeatherStation.HAL;
using Microsoft.Extensions.DependencyInjection;

namespace BrunelUni.WeatherStation.DIModule
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection BindWeatherStationServices( this IServiceCollection services ) =>
            services.BindServices( new Action [ ]
                {
                    HALPiInitializer.Initialize,
                    WeatherStationCoreInitializer.Initialize,
                    DataAccessLayerIntializer.Initialize
                }, DataApplicationConstants.RootNamespace )
                .AddTransient<WeatherContext>( )
                .AddTransient<ILibcAdapter>( x =>
                {
                    if( x.GetService<IConfigurationAdapter>( )
                       .Get<AppOptions>( )
                       .Simulated )
                    {
                        return x.GetService<ISimulatedLibcAdapter>( );
                    }

                    return x.GetService<ILinuxLibcAdapter>( );
                } )
                .AddTransient<ISecureShellService>( x =>
                {
                    if( x.GetService<IConfigurationAdapter>( )
                       .Get<AppOptions>( )
                       .Simulated )
                    {
                        return x.GetService<ISimulatedSecureShellService>( );
                    }

                    return x.GetService<ILinuxSecureShellService>( );
                } )
                .AddTransient<MvcAdapter>( );
    }
}