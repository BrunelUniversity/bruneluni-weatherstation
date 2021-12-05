using System;
using System.Linq;
using System.Threading.Tasks;
using BrunelUni.WeatherStation.Core.Interfaces.Contract;
using BrunelUni.WeatherStation.Crosscutting.DIModule;
using BrunelUni.WeatherStation.HAL.DIModule;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .BindCrosscuttingLayer( )
    .BindHardwareLayer( );
var app = builder.Build();

app.MapGet("/test", ( [ FromServices ] I2CPiServiceFactory i2CPiServiceFactory ) =>
{
    var i2CPiService = i2CPiServiceFactory.Factory( 0x38 );
    
    Task.Delay( 500 );

    var writeData = new byte [ ] { 0xac, 0x33, 0x00 };

    i2CPiService.WriteBytes( writeData );
    Task.Delay( 100 );

    var readBytes = i2CPiService.ReadBytes( 6 ).Value;
        
    var tempRaw = new [ ]
    {
        ( readBytes[ 3 ] & 0x0F ) << 16,
        readBytes[ 4 ] << 8,
        readBytes[ 5 ]
    }.Sum();

    var humRaw = new [ ]
    {
        readBytes[ 1 ] << 12,
        readBytes[ 2 ] << 4,
        ( readBytes[ 3 ] & 0xF0 ) >> 4
    }.Sum();

    var temperature = ( ( tempRaw / Math.Pow( 2, 20 ) ) * ( 200 ) ) - 50;

    var humidity = ( humRaw / Math.Pow( 2, 20 ) ) * ( 100 );
        
    return $"{temperature} {humidity}";
});
app.Run();