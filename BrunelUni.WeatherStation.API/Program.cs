using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const int openReadWrite = 2;
const int i2CSlave = 0x0703;
 
[ DllImport( "libc.so.6", EntryPoint = "open" ) ]
static extern int Open( string fileName, int mode );
 
[ DllImport( "libc.so.6", EntryPoint = "ioctl", SetLastError = true ) ]
static extern int Ioctl( int fd, int request, int data );
 
[ DllImport( "libc.so.6", EntryPoint = "read", SetLastError = true ) ]
static extern int Read( int handle, byte[] data, int length );

[ DllImport( "libc.so.6", EntryPoint = "write", SetLastError = true ) ]
static extern int Write( int handle, byte[] data, int length );



app.MapGet("/test", ( ) =>
{
    try
    {
        var i2CBushandle = Open( "/dev/i2c-1", openReadWrite );

        const int registerAddress = 0x38;
        var deviceReturnCode = Ioctl( i2CBushandle, i2CSlave, registerAddress );

        Task.Delay( 500 );

        var writeData = new byte [ ] { 0xac, 0x33, 0x00 };

        Write( i2CBushandle, writeData, writeData.Length );
        Task.Delay( 100 );

        var secondReadBytes = new byte[ ] { 0x71, 0x00, 0x00, 0x00, 0x00, 0x00 };
        
        Read( i2CBushandle, secondReadBytes, secondReadBytes.Length );

        var humRaw = new [ ]
        {
            secondReadBytes[ 1 ] << 16,
            secondReadBytes[ 2 ] << 8,
            ( secondReadBytes[ 3 ] & 0xF0 ) >> 4 
        }.Sum();
        
        var tempRaw = new [ ]
        {
            ( secondReadBytes[ 3 ] & 0x0F ) << 16,
            secondReadBytes[ 4 ] << 8,
            secondReadBytes[ 5 ]
        }.Sum();

        var returnVal = secondReadBytes.Skip( 1 ).Aggregate( "", ( current, b ) => current + $"byte{Array.IndexOf( secondReadBytes, b )}: {b}" );

        var temperature = ( tempRaw / Math.Pow( 2, 20 ) ) * ( 200 - 50 );

        var humidity = ( humRaw / Math.Pow( 2, 20 ) ) * ( 100 );
        
        return returnVal;
    }
    catch( Exception e )
    {
        return e.Message;
    }
});

app.Run();