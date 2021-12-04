using System;
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
        
        var readBytes = new byte[ ] { 0x71 };

        Read( i2CBushandle, readBytes, readBytes.Length );
        if( ( readBytes[ 0 ] | 0x08 ) == 0 )
        {
            return "error";
        }

        var writeData = new byte [ ] { 0xac, 0x33, 0x00 };

        Write( i2CBushandle, writeData, writeData.Length );
        Task.Delay( 100 );

        var secondReadBytes = new byte[ ] { 0x71, 0x00, 0x00, 0x00, 0x00, 0x00 };
        
        Read( i2CBushandle, secondReadBytes, secondReadBytes.Length );

        var traw = ( ( secondReadBytes[ 3 ] & 0xf ) << 16 ) + ( secondReadBytes[ 4 ] << 8 ) + secondReadBytes[ 5 ];

        var hraw = ( ( secondReadBytes[ 3 ] & 0xf0 ) >> 4 ) + ( secondReadBytes[ 1 ] << 12 ) +
               ( secondReadBytes[ 2 ] << 4 );
        return$"temp {traw} humidity {hraw}";
    }
    catch( Exception e )
    {
        return e.Message;
    }
});

app.Run();