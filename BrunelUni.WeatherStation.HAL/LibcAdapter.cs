using BrunelUni.WeatherStation.Core.Interfaces.Contract;

namespace BrunelUni.WeatherStation.HAL;

public class LibcAdapter : ILibcAdapter
{
    public int Open( string fileName, int mode ) => LibcNativeCommands.OpenNative( fileName, mode );

    public int Ioctl( int fd, int request, int data ) => LibcNativeCommands.IoctlNative( fd, request, data );

    public int Read( int handle, byte [ ] data, int length ) => LibcNativeCommands.ReadNative( handle, data, length );

    public int Write( int handle, byte [ ] data, int length ) => LibcNativeCommands.WriteNative( handle, data, length );
}