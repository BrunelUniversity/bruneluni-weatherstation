using BrunelUni.WeatherStation.Core.Interfaces.Contract;

namespace BrunelUni.WeatherStation.HAL;

public class SimulatedLibcAdapter : ISimulatedLibcAdapter
{
    public int Open( string fileName, int mode ) => 0;

    public int Ioctl( int fd, int request, int data ) => 0;

    public int Read( int handle, byte [ ] data, int length )
    {
        data[ 0 ] = 28;
        data[ 1 ] = 124;
        data[ 2 ] = 190;
        data[ 3 ] = 101;
        data[ 4 ] = 119;
        data[ 5 ] = 88;
        return 0;
    }

    public int Write( int handle, byte [ ] data, int length ) => 0;
}