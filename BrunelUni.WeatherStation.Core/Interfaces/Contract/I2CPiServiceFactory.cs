using Aidan.Common.Core.Interfaces.Excluded;

namespace BrunelUni.WeatherStation.Core.Interfaces.Contract;

public interface I2CPiServiceFactory : IFactory
{
    I2CPiService Factory( int address );
}