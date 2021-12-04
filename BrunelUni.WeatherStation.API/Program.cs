using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using BrunelUni.WeatherStation.API;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const int openReadWrite = 2;
const int i2CSlave = 0x0703;
const int rtldNow = 2;

[ DllImport( "libdl.so", EntryPoint = "dlopen" ) ]
static extern IntPtr LoadLibrary( string filename, int flags );

[ DllImport( "libdl.so", EntryPoint = "dlsym"  ) ]
static extern IntPtr GetProcAddress( IntPtr handle, string symbol );

TFunc loadFunction<TFunc, TWrapper>( ) where TFunc : Delegate
{
    var dllPath = typeof( TWrapper )
        .GetCustomAttribute<LibWrapperAttribute>()?
        .Name;
    var hModule = LoadLibrary( dllPath, rtldNow );
    var functionAddress = GetProcAddress( hModule, typeof( TFunc ).Name.ToLower( ) );
    return Marshal.GetDelegateForFunctionPointer( functionAddress, typeof( TFunc ) ) as TFunc;
}

int open( string fileName, int mode ) => loadFunction<Open, LibCWrapper>( ).Invoke( fileName, mode );

int ioctl( int fd, int request, int data ) => loadFunction<Ioctl, LibCWrapper>( ).Invoke( fd, request, data );

int read( int handle, byte [ ] data, int length ) => loadFunction<Read, LibCWrapper>( ).Invoke( handle, data, length );

int write( int handle, byte [ ] data, int length ) => loadFunction<Write, LibCWrapper>( ).Invoke( handle, data, length );


app.MapGet("/test", ( ) =>
{
    var i2CBushandle = open( "/dev/i2c-1", openReadWrite );

    const int registerAddress = 0x38;
    var deviceReturnCode = ioctl( i2CBushandle, i2CSlave, registerAddress );

    Task.Delay( 500 );

    var writeData = new byte [ ] { 0xac, 0x33, 0x00 };

    write( i2CBushandle, writeData, writeData.Length );
    Task.Delay( 100 );

    var secondReadBytes = new byte[ ] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        
    read( i2CBushandle, secondReadBytes, secondReadBytes.Length );
        
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
});
app.Run();