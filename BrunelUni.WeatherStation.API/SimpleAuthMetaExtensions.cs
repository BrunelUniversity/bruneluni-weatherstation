using Microsoft.AspNetCore.Builder;

namespace BrunelUni.WeatherStation.API;

public static class SimpleAuthMetaExtensions
{
    public static TBuilder AddSimpleAuthGaurd<TBuilder>( this TBuilder builder ) where TBuilder : IEndpointConventionBuilder
    {
        builder.Add( endpointBuilder => { endpointBuilder.Metadata.Add( new SimpleAuthMeta( ) ); } );
        return builder;
    }
}