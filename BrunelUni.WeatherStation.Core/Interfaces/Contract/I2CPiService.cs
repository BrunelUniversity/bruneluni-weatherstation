using Aidan.Common.Core;

namespace BrunelUni.WeatherStation.Core.Interfaces.Contract;

public interface I2CPiService
{
    Result WriteBytes( byte [ ] data );
    ObjectResult<byte [ ]> ReadBytes( int length );
}