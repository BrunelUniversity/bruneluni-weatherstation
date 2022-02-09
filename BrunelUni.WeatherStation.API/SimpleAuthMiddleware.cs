using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace BrunelUni.WeatherStation.API;

public class SimpleAuthMiddleware
{
    private readonly RequestDelegate _next;

    public SimpleAuthMiddleware( RequestDelegate next ) { _next = next; }

    public async Task Invoke( HttpContext context )
    {
        if( context.GetEndpoint( )?.Metadata.GetMetadata<SimpleAuthMeta>( ) == null ) await _next.Invoke( context );
        
        StringValues header;
        var isHeaderPresent = context.Request.Headers.TryGetValue( "Simple-Auth-Key", out header );
        if( !isHeaderPresent )
            await HandleError( context, "no 'Simple-Auth-Key' value was present in the request header" );
    
        var key = Environment.GetEnvironmentVariable( "SIMPLE_AUTH_KEY" );

        if( key == null ) { await HandleError( context, "'Simple-Auth-Key' value was not found in config" ); }
        else
        {
            var normalizedHeader = Regex.Replace( header.ToString( ), @"\s", "" );
            var normalizedKey = Regex.Replace( key, @"\s", "" );

            if( normalizedKey == normalizedHeader )
                await _next.Invoke( context );
            else
                await HandleError( context, "'Simple-Auth-Key' value was not valid" );
        }
    }

    private static async Task HandleError( HttpContext context, string message )
    {
        context
            .Response
            .StatusCode = 401;
        context
            .Response
            .ContentType = "application/json";
        await context
            .Response
            .WriteAsync( JsonConvert.SerializeObject( new { error = "authorization error", message } ) );
    }
}