using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using BrunelUni.WeatherStation.Crosscutting.DIModule;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
builder.Services.BindCrosscuttingLayer( );
var app = builder.Build();

const int openReadWrite = 2;
const int i2CSlave = 0x0703;

const string dllName = "libdl.so";

[ DllImport( dllName, EntryPoint = "open" ) ]
static extern int Open( string fileName, int mode );

[ DllImport( dllName, EntryPoint = "ioctl" ) ]
static extern int Ioctl( int fd, int request, int data );

[ DllImport( dllName, EntryPoint = "read" ) ]
static extern int Read( int handle, byte [ ] data, int length );

[ DllImport( dllName, EntryPoint = "write" ) ]
static extern int Write( int handle, byte [ ] data, int length );

app.MapGet("/test", ( ) =>
{
    var i2CBushandle = Open( "/dev/i2c-1", openReadWrite );

    const int registerAddress = 0x38;
    var deviceReturnCode = Ioctl( i2CBushandle, i2CSlave, registerAddress );

    Task.Delay( 500 );

    var writeData = new byte [ ] { 0xac, 0x33, 0x00 };

    Write( i2CBushandle, writeData, writeData.Length );
    Task.Delay( 100 );

    var secondReadBytes = new byte[ ] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        
    Read( i2CBushandle, secondReadBytes, secondReadBytes.Length );
        
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

    var temperature = ( ( tempRaw / Math.Pow( 2, 20 ) ) * ( 200 ) ) - 50;

    var humidity = ( humRaw / Math.Pow( 2, 20 ) ) * ( 100 );
        
    return $"{temperature} {humidity}";
});
app.Run();