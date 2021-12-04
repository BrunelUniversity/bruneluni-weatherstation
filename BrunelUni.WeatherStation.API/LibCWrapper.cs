namespace BrunelUni.WeatherStation.API;

public delegate int Open( string fileName, int mode );
public delegate int Ioctl( int fd, int request, int data );
public delegate int Read( int handle, byte[] data, int length );
public delegate int Write( int handle, byte[] data, int length );

[ LibWrapper( Name = "libc.so.6" ) ]
public class LibCWrapper
{
}