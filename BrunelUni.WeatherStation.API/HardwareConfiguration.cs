using System.Runtime.InteropServices;

namespace BrunelUni.WeatherStation.API;

[ LibWrapper( Name = "libc.so.6" ) ]
internal static class LibCWrapper
{
    [ DllImport( "libc.so.6", EntryPoint = "open", CharSet = CharSet.Unicode, SetLastError = true )  ]
    public static extern int Open( string fileName, int mode );
 
    [ DllImport( "libc.so.6", EntryPoint = "ioctl", CharSet = CharSet.Unicode, SetLastError = true ) ]
    public static extern int Ioctl( int fd, int request, int data );
 
    [ DllImport( "libc.so.6", EntryPoint = "read", CharSet = CharSet.Unicode, SetLastError = true ) ]
    public static extern int Read( int handle, byte[] data, int length );

    [ DllImport( "libc.so.6", EntryPoint = "write", CharSet = CharSet.Unicode, SetLastError = true ) ]
    public static extern int Write( int handle, byte[] data, int length );
}