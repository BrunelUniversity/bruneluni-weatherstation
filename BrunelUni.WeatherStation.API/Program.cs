using System;
using System.Linq;
using System.Threading.Tasks;
using BrunelUni.WeatherStation.API;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const int openReadWrite = 2;
const int i2CSlave = 0x0703;

app.MapGet("/test", ( ) =>
{
    try
    {
        var i2CBushandle = LibCWrapper.Open( "/dev/i2c-1", openReadWrite );

        const int registerAddress = 0x38;
        var deviceReturnCode = LibCWrapper.Ioctl( i2CBushandle, i2CSlave, registerAddress );

        Task.Delay( 500 );

        var writeData = new byte [ ] { 0xac, 0x33, 0x00 };

        LibCWrapper.Write( i2CBushandle, writeData, writeData.Length );
        Task.Delay( 100 );

        var secondReadBytes = new byte[ ] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        
        LibCWrapper.Read( i2CBushandle, secondReadBytes, secondReadBytes.Length );
        
        var tempRaw = new [ ]
        {
            ( secondReadBytes[ 3 ] & 0x0F ) << 16,
            secondReadBytes[ 4 ] << 8,
            secondReadBytes[ 5 ]
        }.Sum();

        var humRaw = new [ ]
        {
            secondReadBytes[ 1 ] << 12,
            secondReadBytes[ 2 ] << 4,
            ( secondReadBytes[ 3 ] & 0xF0 ) >> 4
        }.Sum();

        var returnVal = secondReadBytes.Skip( 1 ).Aggregate( "", ( current, b ) => current + $"byte{Array.IndexOf( secondReadBytes, b )}: {b} " );

        var temperature = ( ( tempRaw / Math.Pow( 2, 20 ) ) * ( 200 ) ) - 50;

        var humidity = ( humRaw / Math.Pow( 2, 20 ) ) * ( 100 );
        
        return $"{temperature} {humidity}";
    }
    catch( Exception e )
    {
        return e.Message;
    }
});

app.Run();