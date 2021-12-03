using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Unosquare.WiringPi;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/test", ( ) =>
{
    try
    {
        var bus = new I2CBus( );
        var device = bus.AddDevice( 0x38 );
        Task.Delay( 500 );
        var status = device.ReadAddressWord( 0x71 );
        Task.Delay( 100 );
        device.WriteAddressWord( 0xac, 51 );
        Task.Delay( 1000 );
        var data = device.ReadAddressWord( 0x71 );
        return$"status {status} data {data}";
    }
    catch( Exception e )
    {
        return e.Message;
    }
});

app.Run();