using System.Runtime.InteropServices;

namespace BrunelUni.WeatherStation.HAL
{
    public static class LibcNativeCommands
    {
        private const string DllName = "libdl.so";

        [ DllImport( DllName, EntryPoint = "open" ) ]
        internal static extern int OpenNative( string fileName, int mode );

        [ DllImport( DllName, EntryPoint = "ioctl" ) ]
        internal static extern int IoctlNative( int fd, int request, int data );

        [ DllImport( DllName, EntryPoint = "read" ) ]
        internal static extern int ReadNative( int handle, byte [ ] data, int length );

        [ DllImport( DllName, EntryPoint = "write" ) ]
        internal static extern int WriteNative( int handle, byte [ ] data, int length );
    }
}